using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger
{
    public class DataGridViewHistory
    {
        // controls the activity history for a DataGridView 
        // history stacks contain tuple that begins with identifier "cell" or "row"
        // cell format: (string type, int row, int col, string contents)
        // row format: (string type, int row, string note, int quantity, int habitHasDateID)
        // C# stack data structure documentation: https://learn.microsoft.com/en-us/dotnet/api/system.collections.stack?view=net-10.0
        private Stack undoHistory = new Stack();
        private Stack redoHistory = new Stack();

        private DataTable dataGridViewdDt;
        private DbModel sqLiteDb;

        public DataGridViewHistory(DbModel db)
        {
            sqLiteDb = db;
        }

        public void Redo()
        {
            // pop top value off redoHistory
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into undoHistory
            // replace destination cell(s) with values from redoHistory
            // write changes to database
        }

        public void Undo()
        {
            // pop top value off undoHistory
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into redoHistory
            // replace destination cell(s) with values from undoHistory
            // write changes to database
        }

        public void Commit(List<string> values)
        {
            // clear redoHistory stack
            redoHistory.Clear();

            // check length of values 
            // if 3, push tuple into undoHistory with first value "cell"
            if (values.Count == 3)
            {

            }
            // otherwise, push tuple into undoHistory with first value "row"
            // the latter case should only occur during a delete
            // write changes to database
        }

        public void ClearHistory()
        {
            // clear both history stacks with Clear()
        }

        public void SetDT(DataTable dt)
        {
            dataGridViewdDt = dt;
        }
    }
}
