using System;
using System.Runtime.CompilerServices;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;

/*
Database Model class.
Allows access to the database with CRUD operations.
*/
namespace HabitLogger
{
    public class DbModel
    {
        public SQLiteConnection conn;
        private string connString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        //private string connString = "Data Source=../../Database/HabitLoggerDb.db; Version=3; New=True; Compress=True;";

        // default constructor
        public DbModel(string dbFilePath = "../../Database/HabitLoggerDb.db", string ddlFile = "DDL.sql")
        {
            // TODO: move db to project directory
            // DbConnect(dbFile);
            RunDdlFromResourceFile(dbFilePath, ddlFile);
            Console.WriteLine(connString);
        }

        // -----------------------------------------------------
        // Initialize database
        // -----------------------------------------------------
        public void RunDdlFromResourceFile(string dbFilePath, string fileName)
        {
            /*
            Create database tables and optionally run sample inserts from a given DDL file name
            Assumes fileName exists as a resource in the project AND that it contains valid DDL for creating a database
            */

            // exit method if db file already exists
            if (!File.Exists(dbFilePath))
            {
                // establish db connection
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    DbConnect(conn);
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
            }
        }

        // -----------------------------------------------------
        // General Use Methods
        // -----------------------------------------------------
        public void DbConnect(SQLiteConnection conn, [CallerMemberName] string callingMethod = null)
        {
            // attempt to open the connection
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                // notify user and exit the program (Application.Exit() does not work here)
                MessageBox.Show($"{ex.Message}.\nException occurred in {callingMethod}.", "DB Connect Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        // TODO: pass all SQLiteCommand executions through a general use method too?

        // -----------------------------------------------------
        // Users Table Queries
        // -----------------------------------------------------

        public void CreateUser(string userName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                DbConnect(conn);
                // Add User record with userName to Users table
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "INSERT INTO Users (userName) " +
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
            }

        }

        public int ReadUser(string userName)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                DbConnect(conn);
                // get userID given a userName
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    int id = 0;
                    cmd.CommandText = "SELECT userID AS 'userID', " +
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
            }
        }

        // -----------------------------------------------------
        // Habits Table Queries
        // -----------------------------------------------------
        public void CreateHabit(string habitName, string habitDesc, int userID)
        {
            // TODO: need to test this method
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                DbConnect(conn);
                // Add Habit record to Habits with name, description (optional), and user ID
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "INSERT INTO Habits (name, description, userID) " +
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
            }
        }

        public List<(int habitID, string name, string description)> ReadHabitByUser(int userID)
        {
            // CreateHabit option 1: used to list habits in AddHabitForm
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                DbConnect(conn);
                // prepare list of tuples to return
                List<(int habitID, string name, string description)> returnList = new List<(int habitID, string name, string description)>();

                // when date is empty, get habits by userID
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT habitID AS 'habitID', " +
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
        }

        
        // Deprecated READ operation for ListView 
        public List<(int habitID, string name, string note, int habitHasDateID, string quantity)> ReadHabitByDate(int userID, string date)
        {
            // CreateHabit option 2: used to list habits in lblMain of MainForm
            // prepare list of tuples to return
            List<(int habitID, string name, string note, int habitHasDateID, string quantity)> returnList = new List<(int habitID, string name, string description, int habitHasDateID, string quantity)>();

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
                        note: Convert.ToString(read["Note"]),
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
        
        // READ operation for DataGridView
        public DataTable ReadHabitByDateDT(int userID, string date)
        {
            // CreateHabit option 2: used to populate grid view of habits in lblMain of MainForm
            // initialize return DataTable 
            DataTable data = new DataTable();

            // pull up habit by date and userID
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "SELECT h.habitID AS 'habitID', " +
                                       "h.name AS 'Habit', " +
                                       "h.description AS 'Description', " +
                                       "hd.note AS 'Note', " +
                                       "hd.quantity AS 'Frequency', " +
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

            // populate the DataTable
            try
            {
                // instantiate adapter with SQLiteCommand constructor
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                {
                    adapter.Fill(data); // todo: test why order of columns breaks the DataTable
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Read Habit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return data;
        }

        // UpdateHabit method
        // updates habit name or description given a habitID
        public void UpdateHabit(string habitName, string habitDesc, int habitID)
        {
            // Update name and description for the habit with the given habitID
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "UPDATE Habits " +
                                "SET name = :habitName, " +
                                    "description = :habitDesc " +
                                "WHERE Habits.habitID = :habitID;";

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
            cmd.Parameters.AddWithValue(":habitID", habitID);

            // execute query
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Update Habit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DeleteHabit method
        // delete a Habit given its ID, also deletes Dates and intermediate Habits_has_Dates records where appropriate


        // -----------------------------------------------------
        // Dates and Habits_has_Dates Table Queries
        // -----------------------------------------------------

        // CreateDate method
        // creates a Dates record if it doesn't already exist
        public void CreateDate(string date)
        {
            // Add Date to Dates table if it does not exist
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "INSERT INTO Dates (date) " +
                                "SELECT :date " +
                                "WHERE NOT EXISTS (SELECT * FROM Dates WHERE date = :date);";

            // add parametarized values (TODO: should replace all instances of :date with date, need to confirm)
            cmd.Parameters.AddWithValue(":date", date);

            // execute query
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Create Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // TODO: don't need this method? delete if ReadHabitByDateDT sufficient
        // ReadDateAndHabit method
        // gets dateID given a date and returns all habits that occur on that date as a list to be viewed in the lstHabits form

        // CreateHabitHasDate method
        public void CreateHabitHasDate(string note, int quantity, string habitName, string date)
        {
            // Add record to HabitsHasDates table
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "INSERT INTO Habits_has_Dates (quantity, habitID, dateID) " +
                                "VALUES(:note, " +
                                       ":quantity, " +
                                       "(SELECT habitID FROM Habits WHERE name = :habitName), " +
                                       "(SELECT dateID FROM Dates WHERE date = :date));";

            // add parametarized values
            // insert NULL for note if it is blank
            if (note == "")
            {
                cmd.Parameters.AddWithValue(":note", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(":note", note);
            }
            cmd.Parameters.AddWithValue(":quantity", quantity);
            cmd.Parameters.AddWithValue(":habitName", habitName);
            cmd.Parameters.AddWithValue(":date", date);

            // execute query
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Create HabitHasDate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // UpdateHabitsHasDates method
        public void UpdateHabitHasDate(string note, int quantity, int habitHasDateID)
        {
            // given a habitHasDateID, update the quantity column
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText =   "UPDATE Habits_has_Dates " +
                                "SET note = :note, " +
                                    "quantity = :quantity " +
                                "WHERE Habits_has_Dates.habitHasDateID = :habitHasDateID;";

            // add parametarized values
            // insert NULL for note if it is blank
            if (note == "")
            {
                cmd.Parameters.AddWithValue(":note", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue(":note", note);
            }
            cmd.Parameters.AddWithValue(":quantity", quantity);
            cmd.Parameters.AddWithValue(":habitHasDateID", habitHasDateID);

            // execute query
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Update HabitHasDate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // DeleteHabitHasDate method
        // deletes a Habits_has_Dates record given habitHasDateID and also deletes Dates record if appropriate
        public void DeleteHabitHasDate(int habitHasDateID)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Habits_has_Dates " +
                              "WHERE habitHasDateID = :habitHasDateID;";
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
