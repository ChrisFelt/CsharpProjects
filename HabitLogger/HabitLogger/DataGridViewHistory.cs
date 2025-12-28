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
        // C# stack data structure documentation: https://learn.microsoft.com/en-us/dotnet/api/system.collections.stack?view=net-10.0
        private Stack<(int row, string note, int quantity, int habitHasDateID)> undoHistory = new Stack<(int row, string note, int quantity, int habitHasDateID)>();
        private Stack<(int row, string note, int quantity, int habitHasDateID)> redoHistory = new Stack<(int row, string note, int quantity, int habitHasDateID)>();
        
        public DataGridViewHistory() { }  // empty constructor

        public (int row, string note, int quantity, int habitHasDateID) Redo((int row, string note, int quantity, int habitHasDateID) values)
        {
            // pop top value off redoHistory
            (int row, string note, int quantity, int habitHasDateID) returnValue = redoHistory.Pop();
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into undoHistory
            undoHistory.Push(values);
            // replace destination cell(s) with values from redoHistory
            return returnValue;
            // TODO: modify db/dt in calling function
            // write changes to database
        }

        public (int row, string note, int quantity, int habitHasDateID) Undo((int row, string note, int quantity, int habitHasDateID) values)
        {
            // pop top value off undoHistory
            (int row, string note, int quantity, int habitHasDateID) returnValue = undoHistory.Pop();
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into redoHistory
            redoHistory.Push(values);
            // replace destination cell(s) with values from undoHistory
            return returnValue;
            // write changes to database
        }

        public void Commit((int row, string note, int quantity, int habitHasDateID) values)
        {
            // clear redoHistory stack and push values on top of undoHistory
            redoHistory.Clear();
            undoHistory.Push(values);
        }

        public void ClearHistory()
        {
            redoHistory.Clear();
            undoHistory.Clear();
        }
    }
}
