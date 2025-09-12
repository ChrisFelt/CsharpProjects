using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

/*
Database Controller class.
Allows access to the database with CRUD operations.
*/
namespace HabitLogger
{
    public class DbController
    {
        public SQLiteConnection conn;

        // default constructor
        public DbController(string dbFile = "../../database/HabitLoggerDb.db", string ddlFile = "DDL.sql")
        {
            // TODO: move db to project directory
            DbConnect("../../database/HabitLoggerDb.db");
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
            conn = new SQLiteConnection($"Data Source={path}; Version=3; New=True;Compress=True");
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
            cmd.CommandText = $"INSERT INTO Users (userName) VALUES ('{userName}');";
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
            string temp = "";
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT userID AS 'User ID', userName AS 'User Name' FROM Users WHERE userName = '{userName}';";
            SQLiteDataReader read = cmd.ExecuteReader();
            try
            {
                // return userID if userName found
                // Read() feeds the next matching record into the data reader
                if (read.Read())
                {
                    id = Convert.ToInt32(read["User ID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Read User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // return error value to calling function
                return -1;
            }
            Console.WriteLine("No exceptions caught in ReadUser()");
            return id;  // returns 0 when no match found
        }

        // -----------------------------------------------------
        // Habits Table Queries
        // -----------------------------------------------------
        public void CreateHabit(string habitName, string habitDesc, int userID)
        {
            // Add User record with userName to Users table
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"INSERT INTO Habits (name, description, userID) VALUES ('{habitName}', '{habitDesc}', '{userID}');";
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Create Habit Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ReadHabit method
        // returns a list of habits given a userID; for use with btnAdd in MainForm

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

        // DeleteDate method
        // deletes Dates record given a date
    }
}
