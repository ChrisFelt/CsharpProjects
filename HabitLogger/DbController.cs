using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
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
		public DbController(string dbFile= "../../database/HabitLoggerDb.db", string ddlFile="DDL.sql")
		{
			// TODO: move db to project directory
            DbConnect("../../database/HabitLoggerDb.db");
            // TODO: only execute this method if the database is empty
            RunDdlFromResourceFile(ddlFile);
		}

        public void DbConnect(string path)
		{
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

        /* 
        Create database tables and optionally run sample inserts from a given DDL file name
		Assumes fileName exists as a resource in the project AND that it contains valid DDL for creating a database
		*/
        public void RunDdlFromResourceFile(string fileName)
		{
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
                MessageBox.Show($"{ex.Message}", "Username Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

		}

        public int ReadUser(string name)
        {
            // get userID given a userName
            int id = 0;
			SQLiteCommand cmd = conn.CreateCommand();
			cmd.CommandText = $"SELECT userID AS 'User ID', userName AS 'User Name' FROM Users WHERE userName = '{name}';";
			SQLiteDataReader read = cmd.ExecuteReader();

			// return userID if userName found
			// Read() gets the next matching record, but only one record can match given unique userName constraint
			if (read.Read())
			{
				id = Convert.ToInt32(read["User ID"]);
			}
			return id;  // returns 0 when no match found
        }
    }
}
