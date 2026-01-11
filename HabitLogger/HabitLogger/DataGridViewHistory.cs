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
        // fields
        private Stack<(string type, int row, string name, int quantity, string note, int habitHasDateID)> _undoHistory = new Stack<(string type, int row, string name, int quantity, string note, int habitHasDateID)>();
        private Stack<(string type, int row, string name, int quantity, string note, int habitHasDateID)> _redoHistory = new Stack<(string type, int row, string name, int quantity, string note, int habitHasDateID)>();
        private int _deletedRowsCount = 0;

        // properties
        public int DeletedRowsCount
        {
            get
            {
                return _deletedRowsCount;
            }
            set
            {
                _deletedRowsCount = value;
            }
        }

        public DataGridViewHistory() { }  // empty constructor

        public (string type, int row, string name, int quantity, string note, int habitHasDateID) Redo()
        {
            // pop top value off _redoHistory
            (string type, int row, string name, int quantity, string note, int habitHasDateID) values = _redoHistory.Pop();
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into _undoHistory
            _undoHistory.Push(values);
            // replace destination cell(s) with values from _redoHistory
            return values;
            // TODO: modify db/dt in calling function
            // write changes to database
        }

        public (string type, int row, string name, int quantity, string note, int habitHasDateID) Undo()
        {
            // pop top value off _undoHistory
            (string type, int row, string name, int quantity, string note, int habitHasDateID) values = _undoHistory.Pop();
            // find its destination cell(s) in the dt
            // push current values of those cell(s) into _redoHistory
            _redoHistory.Push(values);
            // replace destination cell(s) with values from _undoHistory
            return values;
            // write changes to database
        }

        public void Commit((string type, int row, string name, int quantity, string note, int habitHasDateID) values)
        {
            // clear _redoHistory stack and push values on top of _undoHistory
            _redoHistory.Clear();
            _undoHistory.Push(values);
        }

        public int GetRedoCount()
        {
            return _redoHistory.Count;
        }

        public int GetUndoCount()
        {
            return _undoHistory.Count;
        }

        public (string type, int row, string name, int quantity, string note, int habitHasDateID) UndoPeek()
        {
            // peek at top value of _undoHistory
            return _undoHistory.Peek();
        }

        public (string type, int row, string name, int quantity, string note, int habitHasDateID) RedoPeek()
        {
            // peek at top value of _redoHistory
            return _redoHistory.Peek();
        }

        public void ClearHistory()
        {
            _redoHistory.Clear();
            _undoHistory.Clear();
        }
    }
}
