using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

/*
Database Model class.
Allows access to the database with CRUD operations.
*/
namespace HabitLogger
{
    public class DbModel
    {
        public SQLiteConnection conn;

        // default constructor
        public DbModel(string dbFile = "../../Database/HabitLoggerDb.db", string ddlFile = "DDL.sql")
        {
            // TODO: move db to project directory
            DbConnect(dbFile);
            // TODO: only execute this method if the database is empty
            RunDdlFromResourceFile(ddlFile);
        }

        // -----------------------------------------------------
        // Initialize database
        // -----------------------------------------------------
        public void DbConnect(string path)
        {
            // TODO: close db on program exit
            // reference on design choice: https://stackoverflow.com/questions/5474646/is-it-okay-to-always-leave-a-database-connection-open
            // create db connection and attempt to open
            conn = new SQLiteConnection($"Data Source={path}; Version=3; New=True ; Compress=True");
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                // notify user and exit the program (Application.Exit() does not work here)
                MessageBox.Show($"{ex.Message}", "DB Connect Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        public void RunDdlFromResourceFile(string fileName)
        {
            /*
            Create database tables and optionally run sample inserts from a given DDL file name
            Assumes fileName exists as a resource in the project AND that it contains valid DDL for creating a database
            */
            SQLiteCommand cmd = conn.CreateCommand();

            // get array of resource names from the assembly and find matching resource to fileName
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resourceArray = assembly.GetManifestResourceNames();
            string resourceName = resourceArray.FirstOrDefault(str => str.EndsWith($"{fileName}"));

            // read contents of resource to cmd
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    cmd.CommandText = reader.ReadToEnd();
                }
            }

            // execute DDL
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "DDL Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -----------------------------------------------------
        // Users Table Queries
        // -----------------------------------------------------

        public void CreateUser(string userName)
        {
            // Add User record with userName to Users table
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "INSERT INTO Users (userName) " +
                                "VALUES (:userName);";
            cmd.Parameters.AddWithValue(":userName", userName);

            // execute query
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // TODO: make error popup messages more user friendly
                MessageBox.Show($"{ex.Message}", "Create User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int ReadUser(string userName)
        {
            // get userID given a userName
            int id = 0;
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "SELECT userID AS 'userID', " +
                                       "userName AS 'User Name' " +
                                "FROM Users " +
                                "WHERE userName = :userName;";
            cmd.Parameters.AddWithValue(":userName", userName);

            // execute query
            try
            {
                SQLiteDataReader read = cmd.ExecuteReader();
                // return userID if userName found
                // Read() feeds the next matching record into the data reader
                if (read.Read())
                {
                    id = Convert.ToInt32(read["userID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Read User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // return error value to calling function
                return -1;
            }
            return id;  // returns 0 when no match found
        }

        // -----------------------------------------------------
        // Habits Table Queries
        // -----------------------------------------------------
        public void CreateHabit(string habitName, string habitDesc, int userID)
        {
            // Add Habit record to Habits with name, description (optional), and user ID
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "INSERT INTO Habits (name, description, userID) " +
                                "VALUES (:habitName, " +
                                        ":habitDesc, " +
                                        ":userID);";

            // add parameterized values
            cmd.Parameters.AddWithValue(":habitName", habitName);
            // insert NULL for description if it is blank
            if (habitDesc == "")
            {
                cmd.Parameters.AddWithValue(":habitDesc", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(":habitDesc", habitDesc);
            }
            cmd.Parameters.AddWithValue(":userID", userID);

            // execute query
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Create Habit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<(int habitID, string name, string description)> ReadHabitByUser(int userID)
        {
            // CreateHabit option 1: used to list habits in AddHabitForm
            // prepare list of tuples to return
            List<(int habitID, string name, string description)> returnList = new List<(int habitID, string name, string description)>();

            // when date is empty, get habits by userID
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "SELECT habitID AS 'habitID', " +
                                       "name AS 'Name', " +
                                       "description AS 'Description' " +
                                "FROM Habits " +
                                "WHERE userID = :userID;";
            cmd.Parameters.AddWithValue(":userID", userID);


            // populate the return list from data reader
            try
            {
                SQLiteDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    returnList.Add(
                        (habitID: Convert.ToInt32(read["habitID"]),
                        name: Convert.ToString(read["Name"]),
                        description: Convert.ToString(read["Description"]))
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Read Habit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return returnList;
        }

        public List<(int habitID, string name, string description, int habitHasDateID, string quantity)> ReadHabitByDate(int userID, string date)
        {
            // CreateHabit option 2: used to list habits in lblMain of MainForm
            // prepare list of tuples to return
            List<(int habitID, string name, string description, int habitHasDateID, string quantity)> returnList = new List<(int habitID, string name, string description, int habitHasDateID, string quantity)>();

            // pull up habit by date and userID
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "SELECT h.habitID AS 'habitID', " +
                                       "h.name AS 'Name', " +
                                       "h.description AS 'Description', " +
                                       "hd.note AS 'Note', " +
                                       "hd.quantity AS 'Quantity', " +
                                       "hd.habitHasDateID AS 'habitHasDateID' " +
                                "FROM Dates AS d " +
                                "INNER JOIN Habits_has_Dates AS hd " +
                                    "ON d.dateID = hd.dateID " +
                                "INNER JOIN Habits AS h " +
                                    "ON hd.habitID = h.habitID " +
                                "WHERE d.date = :date " +
                                    "AND h.UserID = :userID;";
            cmd.Parameters.AddWithValue(":date", date);
            cmd.Parameters.AddWithValue(":userID", userID);

            // populate the return list from data reader
            try
            {
                SQLiteDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    returnList.Add(
                        (habitID: Convert.ToInt32(read["habitID"]),
                        name: Convert.ToString(read["Name"]),
                        description: Convert.ToString(read["Description"]),
                        habitHasDateID: Convert.ToInt32(read["habitHasDateID"]),
                        quantity: Convert.ToString(read["Quantity"]))
                        );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Read Habit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return returnList;
        }

        // UpdateHabit method
        // updates habit name or description given a habitID

        // DeleteHabit method
        // delete a Habit given its ID, also deletes Dates and intermediate Habits_has_Dates records where appropriate


        // -----------------------------------------------------
        // Dates and Habits_has_Dates Table Queries
        // -----------------------------------------------------

        // CreateDate method
        // creates a Dates record if it doesn't already exist and establishes relationship to Habits via intermediate table Habits_has_Dates

        // ReadDateAndHabit method
        // gets dateID given a date and returns all habits that occur on that date as a list to be viewed in the lstHabits form

        // UpdateHabitsHasDates method
        // given a habitHasDateID, updates the quantity column

        // DeleteHabitHasDate method
        // deletes a Habits_has_Dates record given habitHasDateID and also deletes Dates record if appropriate
        public void DeleteHabitHasDate(int habitHasDateID)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Habits_has_Dates " +
                              "WHERE habitHasDateID = :habitHasDateID; ";
            cmd.Parameters.AddWithValue(":habitHasDateID", habitHasDateID);

            // execute query
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Delete HabitHasDate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DeleteDate method
        // deletes Dates record given a date
    }
}
