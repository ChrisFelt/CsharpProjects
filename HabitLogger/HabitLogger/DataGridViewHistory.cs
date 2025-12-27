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
        // history stacks contain tuple that begins with identifier "cell" or "row"  TODO: probably don't need identifier, delete?
        // C# stack data structure documentation: https://learn.microsoft.com/en-us/dotnet/api/system.collections.stack?view=net-10.0
        private Stack<(string type, int row, string note, int quantity, int habitHasDateID)> undoHistory = new Stack<(string type, int row, string note, int quantity, int habitHasDateID)>();
        private Stack<(string type, int row, string note, int quantity, int habitHasDateID)> redoHistory = new Stack<(string type, int row, string note, int quantity, int habitHasDateID)>();
        
        public DataGridViewHistory() { }  // empty constructor

        // TODO: add public methods RedoCell, RedoRow, UndoCell, and UndoRow. Make Redo and Undo private. Add "cell" and "row" as class variables
        public (string type, int row, string note, int quantity, int habitHasDateID) Redo((string type, int row, string note, int quantity, int habitHasDateID) values)
        {
            // pop top value off redoHistory
            (string type, int row, string note, int quantity, int habitHasDateID) returnValue = redoHistory.Pop();
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into undoHistory
            undoHistory.Push(values);
            // replace destination cell(s) with values from redoHistory
            return returnValue;
            // TODO: modify db/dt in calling function
            // write changes to database
        }

        public (string type, int row, string note, int quantity, int habitHasDateID) Undo((string type, int row, string note, int quantity, int habitHasDateID) values)
        {
            // pop top value off undoHistory
            (string type, int row, string note, int quantity, int habitHasDateID) returnValue = undoHistory.Pop();
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into redoHistory
            redoHistory.Push(values);
            // replace destination cell(s) with values from undoHistory
            return returnValue;
            // write changes to database
        }

        public void Commit((string type, int row, string note, int quantity, int habitHasDateID) values)
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
