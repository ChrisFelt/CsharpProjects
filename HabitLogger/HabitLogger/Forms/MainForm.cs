using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HabitLogger
{
    public partial class main : Form
    {
        // fields
        private DbModel sqliteDb = new DbModel();
        private int curUserID = 0;  // user not logged in
        private string prevCellContents;  // track contents of a cell before edit

        // data source for gridViewHabitsByDate
        private DataTable gridViewHabitsByDateDT = new DataTable();

        // data source for gridViewHabitsByUser
        private DataTable gridViewHabitsByUserDT = new DataTable();

        // gridViewHabitsByUser columns
        private const int habitIDCol = 0;
        private const int habitNameColByUser = 1;
        private const int descriptionCol = 2;

        // gridViewHabitsByDate columns
        private const int habitNameColByDate = 0;
        private const int quantityCol = 1;
        private const int noteCol = 2;
        private const int habitHasDateIDCol = 4;

        // track gridViewHabitsByDate row/cell history separately
        private DataGridViewHistory gridViewHabitsByDateHistory = new DataGridViewHistory();

        // hard code history types
        private const string cellType = "cell";
        private const string rowType = "row";

        // initialize list of current user's habits
        List<(int habitID, string name, string description)> curUserHabits = new List<(int habitID, string name, string description)>();

        public main()
        {
            InitializeComponent();
            CenterToScreen();
            // show only login panel when app is started
            pnlLogin.Show();
            pnlMain.Hide();
        }

        // -----------------------------------------------------
        // pnlLogin General Events
        // -----------------------------------------------------

        // Login button click event
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // validate username input
            string inputTxt = txtUserName.Text;
            if (string.IsNullOrWhiteSpace(inputTxt))
            {
                MessageBox.Show("Invalid Username.\nPlease enter one or more characters.", "Username Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Select();
            }
            else
            {
                // attempt to login
                inputTxt = inputTxt.Trim(' ');
                curUserID = sqliteDb.ReadUser(inputTxt);
                //Console.WriteLine($"Current user ID: {curUserID}");  // testing ReadUser

                if (curUserID > 0)
                {
                    // login success, swap to pnlMain
                    pnlLogin.Hide();
                    pnlMain.Show();

                    // clear txtUserName for next login
                    txtUserName.Clear();
                    // display current username on pnlMain
                    lblDisplayUser.Text = inputTxt;

                    // populate curUserHabits
                    curUserHabits = sqliteDb.ReadHabitByUser(curUserID);

                    // establish data source for gridViewHabitsByDate
                    RefreshGridViewHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));
                    RefreshGridViewHabitsByUser(curUserID);
                }
                else if (curUserID == 0)
                {
                    // login failed, notify user
                    MessageBox.Show($"Username: '{inputTxt}' does not exist.\nPlease try again.", "Login failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtUserName.Select();
                }
                else
                {
                    // reset curUserID to 0 if ReadUser() exited with an exception
                    curUserID = 0;
                }
            }
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // fire btnLogin_Click event on Enter key press
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;  // suppress ding sound
                btnLogin.PerformClick();
            }
        }

        // Exit button click event
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // New User label click event
        private void lblNewUser_Click(object sender, EventArgs e)
        {
            // open NewUserForm window
            NewUserForm newUser = new NewUserForm(sqliteDb);
            newUser.Show();
        }

        // -----------------------------------------------------
        // pnlMain Events
        // -----------------------------------------------------
        // Logout button click event
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // logout user and swap back to login panel
            pnlLogin.Show();
            pnlMain.Hide();
            curUserID = 0;
            txtUserName.Select();
            lblDisplayUser.Text = "";
        }

        // Date click event
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            RefreshGridViewHabitsByDate(e.Start.ToString("yyyy-MM-dd"));
            // TODO: There is currently no way to delete a date. Add delete date option?
            
            // reset history and grey out undo/redo text
            gridViewHabitsByDateHistory.ClearHistory();

            btnUndo.ForeColor = SystemColors.ControlDark;
            btnRedo.ForeColor = SystemColors.ControlDark;
        }

        // Undo click event:
        // 1. pops top value off of undoHistory and replaces the current value of that cell with the undoHistory value
        // 2. pushes the value into redoHistory
        // Redo click event does the reverse
        private void btnUndo_Click(object sender, EventArgs e)
        {
            // do nothing if undo stack is empty
            if (gridViewHabitsByDateHistory.GetUndoCount() > 0)
            {
                // get Undo row number by peeking at the Undo stack
                (string type, int row, string habitName, int quantity, string note, int habitHasDateID) undoData = gridViewHabitsByDateHistory.UndoPeek();

                string type = undoData.type;

                // cell type - gather current values of the row then call Undo method
                if (type == cellType)
                {
                    // get row number of matching habitHasDateID
                    int row = FindRowByHabitHasDateID(undoData.habitHasDateID);
                    string habitName = gridViewHabitsByDate.Rows[row].Cells[habitNameColByDate].Value.ToString();
                    int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[row].Cells[quantityCol].Value.ToString());
                    string note = gridViewHabitsByDate.Rows[row].Cells[noteCol].Value.ToString();
                    int habitHasDateID = undoData.habitHasDateID;

                    undoData = gridViewHabitsByDateHistory.Undo((type, row, habitName, quantity, note, habitHasDateID));

                    cellHistory((undoData.row, undoData.habitName, undoData.quantity, undoData.note, undoData.habitHasDateID));
                }

                // row type - history is updated only after new Habits_Has_Dates record is created
                else
                {
                    // TODO: need to account for multiple simultaneous deleted rows - loop method call for deletedrowscount?
                    rowHistory((undoData.row, undoData.habitName, undoData.quantity, undoData.note));
                }

                // grey out button text when undo history is empty
                if (gridViewHabitsByDateHistory.GetUndoCount() == 0)
                {
                    btnUndo.ForeColor = SystemColors.ControlDark;
                }
                // undo action may somtimes abort, so check here if redo button text needs to be activated
                if (gridViewHabitsByDateHistory.GetRedoCount() > 0)
                {
                    btnRedo.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            // do nothing if redo stack is empty
            if (gridViewHabitsByDateHistory.GetRedoCount() > 0)
            {
                // get Redo row number by peeking at the Redo stack
                (string type, int row, string habitName, int quantity, string note, int habitHasDateID) redoData = gridViewHabitsByDateHistory.RedoPeek();

                string type = redoData.type;

                // grab current values of the row
                // get row number of matching habitHasDateID
                int row = FindRowByHabitHasDateID(redoData.habitHasDateID);
                string habitName;
                int quantity;
                string note;
                int habitHasDateID;

                // get row data from history if dt is empty
                if (gridViewHabitsByDateDT.Rows.Count == 0)
                {
                    habitName = redoData.habitName;
                    quantity = redoData.quantity;
                    note = redoData.note;
                    habitHasDateID = redoData.habitHasDateID;
                }
                // get row data from the gridview
                else
                {
                    habitName = gridViewHabitsByDate.Rows[row].Cells[habitNameColByDate].Value.ToString();
                    quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[row].Cells[quantityCol].Value.ToString());
                    note = gridViewHabitsByDate.Rows[row].Cells[noteCol].Value.ToString();
                    habitHasDateID = redoData.habitHasDateID;
                }

                // cell type - gather current values of the row then call Redo method
                if (type == cellType)
                {
                    redoData = gridViewHabitsByDateHistory.Redo((type, row, habitName, quantity, note, habitHasDateID));

                    cellHistory((redoData.row, redoData.habitName, redoData.quantity, redoData.note, redoData.habitHasDateID));
                }

                // row type - delete the row and update history
                else
                {
                    // TODO: may need to repeat method call for multiple simultaneous deletes
                    DeleteRow(habitHasDateID);

                    // need to refresh the grid view here unlike delete event handlers
                    RefreshGridViewHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));

                    gridViewHabitsByDateHistory.Redo((type, row, habitName, quantity, note, habitHasDateID));
                }

                // grey out button text when redo history is empty
                if (gridViewHabitsByDateHistory.GetRedoCount() == 0)
                {
                    btnRedo.ForeColor = SystemColors.ControlDark;
                }
                // redo action may somtimes abort, so check here if redo button text needs to be activated
                if (gridViewHabitsByDateHistory.GetUndoCount() > 0)
                {
                    btnUndo.ForeColor = SystemColors.ControlText;
                }
            }
        }

        // update all columns in a row
        private void cellHistory((int row, string habitName, int quantity, string note, int habitHasDateID) cellData)
        {
            // update cell values
            gridViewHabitsByDate.Rows[cellData.row].Cells[habitNameColByDate].Value = cellData.habitName;
            gridViewHabitsByDate.Rows[cellData.row].Cells[noteCol].Value = cellData.note;
            gridViewHabitsByDate.Rows[cellData.row].Cells[quantityCol].Value = cellData.quantity;
            gridViewHabitsByDate.Rows[cellData.row].Cells[habitHasDateIDCol].Value = cellData.habitHasDateID;  // TODO: only needed if habit name can be modified

            // update db
            UpdateGridViewHabitsByDateRow(cellData.row);
        }

        // add row to gridViewHabitsByDate and create Habits_Has_Dates relationship OR remove row if it already exists
        private void rowHistory((int row, string habitName, int quantity, string note) rowData)
        {
            // habitHasDateID may vary depending on the action to take, so we declare it beforehand
            int habitHasDateID;

            // attempt to add habit if it no longer exists
            if (!curUserHabits.Any(habit => habit.name == rowData.habitName))
            {
                // if user cancels add habit, do not add the row
                if (!OpenAddHabitForm(rowData.habitName, rowData.row))
                {
                    gridViewHabitsByDate.Rows.Remove(gridViewHabitsByDate.Rows[rowData.row]);
                    return;
                }
            }

            // delete row if it already exists (occurs when user adds a new row and clicks undo button)
            foreach (DataRow dtRow in gridViewHabitsByDateDT.Rows)
            {
                if (dtRow.RowState != DataRowState.Deleted && dtRow[habitNameColByDate].ToString() == rowData.habitName)
                {
                    habitHasDateID = Convert.ToInt32(dtRow[habitHasDateIDCol]);
                    gridViewHabitsByDateHistory.Undo((rowType, rowData.row, rowData.habitName, rowData.quantity, rowData.note, habitHasDateID));
                    DeleteRow(habitHasDateID);

                    // need to refresh the grid view here unlike delete event handlers
                    RefreshGridViewHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));
                    
                    // exit function
                    return;
                }
            }

            // insert a new row into gridViewHabitsByDate
            DataRow newRow = gridViewHabitsByDateDT.NewRow();

            // update cell values
            newRow[habitNameColByDate] = rowData.habitName;
            newRow[quantityCol] = rowData.quantity;
            newRow[noteCol] = rowData.note;

            gridViewHabitsByDateDT.Rows.Add(newRow);

            // create Habits_Has_Dates relationship - new row will always be the last populated row in the datagridview
            CreateGridViewHabitsByDateRow(rowData.habitName, gridViewHabitsByDate.Rows.Count - 2);

            // update history here to reflect the new habitHasDateID
            DataRow dtLastRow = gridViewHabitsByDateDT.Rows[gridViewHabitsByDateDT.Rows.Count - 1];
            string habitName = dtLastRow[habitNameColByDate].ToString();
            int quantity = Convert.ToInt32(dtLastRow[quantityCol]);
            string note = dtLastRow[noteCol].ToString();
            habitHasDateID = Convert.ToInt32(dtLastRow[habitHasDateIDCol]);

            gridViewHabitsByDateHistory.Undo((rowType, gridViewHabitsByDateDT.Rows.Count - 1, habitName, quantity, note, habitHasDateID));
        }

        // -----------------------------------------------------
        // pnlMain gridViewHabitsByUser Events
        // -----------------------------------------------------
        // CellBeginEdit event
        // grab current contents of the cell
        private void gridViewHabitsByUser_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // save contents of cell before editing
            Console.WriteLine($"Editing HabitsByUser cell at row {e.RowIndex} and column {e.ColumnIndex}");
            prevCellContents = gridViewHabitsByUser.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            Console.WriteLine($"Contents: {prevCellContents}");
        }

        // CellEndEdit event
        // calls UpdateHabit method if on existing row - no validation required
        // repopulates curUserHabits
        private void gridViewHabitsByUser_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: trim white space from user input!

            string habitName = gridViewHabitsByUser.Rows[e.RowIndex].Cells[habitNameColByUser].Value.ToString();
            string description = gridViewHabitsByUser.Rows[e.RowIndex].Cells[descriptionCol].Value.ToString();

            string curCellContents = gridViewHabitsByUser.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            // add habit if habit name was entered on new row
            if (e.RowIndex == gridViewHabitsByUser.Rows.Count)
            {
                if (habitName != "" && OpenAddHabitForm(habitName, e.RowIndex, description))
                {
                    // repopulate current user's habits
                    curUserHabits = sqliteDb.ReadHabitByUser(curUserID);

                    // refresh DGV
                    RefreshGridViewHabitsByUser(curUserID);
                }

                // otherwise delete the new row if it was added
                else if (e.RowIndex != gridViewHabitsByUser.Rows.Count - 1)
                {
                    gridViewHabitsByUser.Rows.Remove(gridViewHabitsByUser.Rows[e.RowIndex]);
                    Console.WriteLine("New row was removed: no habit name/user exited AddHabitForm without saving.");
                }
            }

            // update habit if current cell modified
            else if (prevCellContents != curCellContents)
            {
                sqliteDb.UpdateHabit(habitName, description, Convert.ToInt32(gridViewHabitsByUser.Rows[e.RowIndex].Cells[habitIDCol].Value));

                // repopulate current user's habits
                curUserHabits = sqliteDb.ReadHabitByUser(curUserID);

                // refresh both DGVs
                RefreshGridViewHabitsByUser(curUserID);
                RefreshGridViewHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));

                Console.WriteLine($"Previous cell contents '{prevCellContents}' replaced with: '{curCellContents}'.");
            }
        }

        // UserDeletingRow event
        // does not support multiple simultaneous deletes
        private void gridViewHabitsByUser_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int habitID = Convert.ToInt32(gridViewHabitsByUser.Rows[e.Row.Index].Cells[habitIDCol].Value);
            string habitName = gridViewHabitsByUser.Rows[e.Row.Index].Cells[habitNameColByUser].Value.ToString();
            string description = gridViewHabitsByUser.Rows[e.Row.Index].Cells[descriptionCol].Value.ToString();
            Console.WriteLine($"User deleting HabitsByUser row: {e.Row.Index} with contents: {habitID} {habitName} {description}.");

            // delete row from db
            sqliteDb.DeleteHabit(habitID);

            // refresh both gridViewHabitsByDate
            RefreshGridViewHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));
        }

        // -----------------------------------------------------
        // pnlMain gridViewHabitsByDate Events
        // -----------------------------------------------------

        // grab contents of the cell before user edits it
        private void gridViewHabitsByDate_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Console.WriteLine($"Editing HabitsByDate cell at row {e.RowIndex} and column {e.ColumnIndex}");
            prevCellContents = gridViewHabitsByDate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            Console.WriteLine($"Contents: {prevCellContents}");
        }

        // validate edits made to a cell and update DataGridViewHistory
        private void gridViewHabitsByDate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 1. add new row - check if cell is on new row
            if (e.RowIndex == gridViewHabitsByDateDT.Rows.Count)
            {
                // exit event without commiting data to db if no habit name entered
                if (gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitNameColByDate].Value.ToString() != "")
                {
                    // set quantity to 0 if quantity column is empty (NOTE: will always be empty until delay committing row to db feature is added)
                    if (gridViewHabitsByDate.Rows[e.RowIndex].Cells[quantityCol].Value.ToString() == "")
                    {
                        gridViewHabitsByDate.Rows[e.RowIndex].Cells[quantityCol].Value = 0;
                    }

                    // get habit row data
                    string habitName = gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitNameColByDate].Value.ToString();
                    int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[quantityCol].Value);
                    string note = gridViewHabitsByDate.Rows[e.RowIndex].Cells[noteCol].Value.ToString();

                    // if habit name does not exist, prompt user to create new habit
                    if (curUserHabits.Any(habit => habit.name == habitName))
                    {
                        // create new Habits_Has_Dates record in db and add to history
                        CreateGridViewHabitsByDateRow(habitName, e.RowIndex);

                        int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitHasDateIDCol].Value);
                        CommitChanges(rowType, e.RowIndex, habitName, quantity, note, habitHasDateID);
                    }
                    else
                    {
                        // popup asks user if they want to add new habit
                        // if yes, open AddHabitForm
                        // if no, delete row
                        if (OpenAddHabitForm(habitName, e.RowIndex))
                        {
                            // add row to date and refresh datagridview
                            CreateGridViewHabitsByDateRow(habitName, e.RowIndex);

                            // update current user habits list
                            curUserHabits = sqliteDb.ReadHabitByUser(curUserID);

                            // add new row to history
                            int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitHasDateIDCol].Value);
                            CommitChanges(rowType, e.RowIndex, habitName, quantity, note, habitHasDateID);
                        }
                        else
                        {
                            gridViewHabitsByDate.Rows.Remove(gridViewHabitsByDate.Rows[e.RowIndex]);
                            Console.WriteLine("New row was removed: user exited AddHabitForm without saving.");
                        }
                    }
                }

                // delete new row from the DGV if it was added
                else if (e.RowIndex != gridViewHabitsByDate.Rows.Count - 1)
                {
                    // TODO: notify user that they must enter a habit name for row to be saved?
                    // gridViewHabitsByDate.AllowUserToAddRows = false;  // causes infinite loop
                    gridViewHabitsByDate.Rows.Remove(gridViewHabitsByDate.Rows[e.RowIndex]);
                    Console.WriteLine("New row was removed: no habit name column value.");
                }
            }
            // 2. edit existing row
            else
            {
                string curCellContents = gridViewHabitsByDate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Console.WriteLine($"Finished editing cell at row {e.RowIndex} and column {e.ColumnIndex}");
                Console.WriteLine($"Previous contents were: '{prevCellContents}'. New contents are: '{curCellContents}'");

                // habit name column can't be edited - roll back value when user attempts to edit it
                // TODO: non-intrusive notification that this column can't be edited
                if (e.ColumnIndex == habitNameColByDate)
                {
                    gridViewHabitsByDate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = prevCellContents;
                }

                // commit all valid edits to db and update history (note: all values are valid for note column)
                else if (prevCellContents != curCellContents)
                {
                    // TODO: do not write to db unless both habit name and quantity columns have a value
                    UpdateGridViewHabitsByDateRow(e.RowIndex);

                    // determine previous cell contents and commit to history
                    string habitName = gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitNameColByDate].Value.ToString();
                    int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[quantityCol].Value);
                    string note = gridViewHabitsByDate.Rows[e.RowIndex].Cells[noteCol].Value.ToString();
                    int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitHasDateIDCol].Value);

                    // assign prevCellContents to appropriate variable
                    switch (e.ColumnIndex)
                    {
                        case habitNameColByDate:
                            habitName = prevCellContents;
                            break;
                        case quantityCol:
                            quantity = Convert.ToInt32(prevCellContents);
                            break;
                        case noteCol:
                            note = prevCellContents;
                            break;
                    }

                    // update history, toggle Undo button text to active, and toggle Redo button text to inactive
                    CommitChanges(cellType, e.RowIndex, habitName, quantity, note, habitHasDateID);
                    Console.WriteLine($"Added to history: {gridViewHabitsByDateHistory.UndoPeek()}");
                }
            }
        }

        // catch data type errors in gridViewHabitsByDate
        private void gridViewHabitsByDate_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            // get the new value entered (can't get this value with Value.ToString())
            string newCellContents = gridViewHabitsByDate.EditingControl.Text;

            // handle non-integer input errors into the habit quantity column
            if (anError.ColumnIndex == quantityCol)
            {
                // roll the value back and notify user
                gridViewHabitsByDate.EditingControl.Text = prevCellContents;
                MessageBox.Show($"Error: frequency must be an integer!\nYou entered: '{newCellContents}'.\nPlease try again.", "Frequency input failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // notify user of unexpected error in a different column
            else
            {
                gridViewHabitsByDate.EditingControl.Text = prevCellContents;
                MessageBox.Show($"Error: an unexpected error has occurred \nat row: {anError.RowIndex} \nand column: {anError.ColumnIndex} \nwith current contents: {newCellContents} \nand previous contents: {prevCellContents}.\nError context: {anError.Context}", "Unexpected Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // add rows deleted from gridViewHabitsByDate to history
        private void gridViewHabitsByDate_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string habitName = gridViewHabitsByDate.Rows[e.Row.Index].Cells[habitNameColByDate].Value.ToString();
            int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[e.Row.Index].Cells[quantityCol].Value);
            string note = gridViewHabitsByDate.Rows[e.Row.Index].Cells[noteCol].Value.ToString();
            int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[e.Row.Index].Cells[habitHasDateIDCol].Value);
            Console.WriteLine($"User deleting row: {e.Row.Index} with contents: {habitName} {quantity} {note} {habitHasDateID}.");
            
            // NOTE: this event fires for EACH row deleted. Multiple rows deleted simultaneously each cause the event to fire individually.
            CommitChanges(rowType, e.Row.Index, habitName, quantity, note, habitHasDateID);
            
            // placeholder - currently history only supports deleting one row at a time
            gridViewHabitsByDateHistory.DeletedRowsCount++;
            Console.WriteLine($"Deleted row count: {gridViewHabitsByDateHistory.DeletedRowsCount}");
            gridViewHabitsByDateHistory.DeletedRowsCount = 0;

            // delete row from db
            DeleteRow(habitHasDateID);
        }

        // autocomplete for new habit in habit name column of gridViewHabitsByDate
        private void gridViewHabitsByDate_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // enable autocomplete for new rows in the habit name column
            if (gridViewHabitsByDate.CurrentCell.ColumnIndex == habitNameColByDate && gridViewHabitsByDate.CurrentCell.RowIndex == gridViewHabitsByDate.Rows.Count - 1)
            {
                // cast the control for this cell as a TextBox
                TextBox autoComplete = e.Control as TextBox;

                // setup autocomplete mode and custom source
                autoComplete.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                autoComplete.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection customSource = new AutoCompleteStringCollection();

                // populate customSource with habit names from curUserHabits
                foreach ((int habitID, string name, string description) tuple in curUserHabits)
                {
                    customSource.Add(tuple.name);
                    Console.WriteLine($"Added habit: {tuple.name} to autocomplete.");
                }
                autoComplete.AutoCompleteCustomSource = customSource;
            }
        }

        // -----------------------------------------------------
        // pnlMain General Use Methods
        // -----------------------------------------------------
        // refresh gridViewHabitsByUser
        private void RefreshGridViewHabitsByUser(int userID)
        {
            gridViewHabitsByUserDT.Clear();

            // populate the dt from the db
            gridViewHabitsByUserDT = sqliteDb.ReadHabitByUserDT(userID);

            // bind to the DGV
            gridViewHabitsByUser.DataSource = gridViewHabitsByUserDT;

            // hide ID
            gridViewHabitsByUser.Columns["habitID"].Visible = false;
        }
        
        // refresh gridViewHabitsByDate
        private void RefreshGridViewHabitsByDate(string date)
        {
            // infinite loop can sometimes occur if DataTable isn't cleared first
            gridViewHabitsByDateDT.Clear();

            // get populated DataTable from db for this date
            gridViewHabitsByDateDT = sqliteDb.ReadHabitByDateDT(curUserID, date);

            gridViewHabitsByDate.DataSource = gridViewHabitsByDateDT;

            // hide IDs and description
            gridViewHabitsByDate.Columns["habitID"].Visible = false;
            gridViewHabitsByDate.Columns["Description"].Visible = false;
            gridViewHabitsByDate.Columns["habitHasDateID"].Visible = false;

            // TODO: allow user to enter habit name in new row under name column, then query db for habit by that name (not case-specific)
            // if habit exists, generate new row automatically with the habit and a frequency of 0
            // if it does not exist, delete text in the new row and notify user with popup asking if they want to create the habit

        }

        // call UpdateHabitHasDate on a given DataGridView row
        private void UpdateGridViewHabitsByDateRow(int row)
        {
            // get values from the given row
            string note = gridViewHabitsByDate.Rows[row].Cells[noteCol].Value.ToString();
            int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[row].Cells[quantityCol].Value.ToString());
            int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[row].Cells[habitHasDateIDCol].Value.ToString());

            // update db
            Console.Write($"Attempting to write note: {note}, quantity: {quantity}, habitHasDateID: {habitHasDateID}... ");
            sqliteDb.UpdateHabitHasDate(note, quantity, habitHasDateID);
            Console.WriteLine("Success!");
            RefreshGridViewHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));
        }

        // delete row from HabitsHasDates
        private void DeleteRow(int habitHasDateID)
        {
            sqliteDb.DeleteHabitHasDate(habitHasDateID);
            Console.WriteLine($"Successfully deleted HabitsHasDates row with ID: {habitHasDateID}.");
            
            // TODO: may not need this line anymore
            // need to refresh the data table to avoid deleted row inaccessible exception
            //RefreshGridViewHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));
        }

        // call CreateHabitHasDate on a given gridViewHabitsByDate row
        private void CreateGridViewHabitsByDateRow(string habitName, int row)
        {
            // grab cell values for the row besides name
            int newRowQuantity = Convert.ToInt32(gridViewHabitsByDate.Rows[row].Cells[quantityCol].Value.ToString());
            string newRowNote = gridViewHabitsByDate.Rows[row].Cells[noteCol].Value.ToString();
            string newRowDate = monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd");

            // call CreateDate (method only adds date if it doesn't already exist)
            sqliteDb.CreateDate(newRowDate);

            // create new Habits_Has_Dates record
            Console.Write($"Creating new HabitHasDate record with: " +
                              $"habit name: {habitName}, " +
                              $"quantity: {newRowQuantity}, " +
                              $"note: {newRowNote}, " +
                              $"date: {newRowDate}... ");
            sqliteDb.CreateHabitHasDate(habitName,
                                        newRowQuantity,
                                        newRowNote,
                                        newRowDate);
            Console.WriteLine("Success!");

            // call RefreshGridViewHabitsByDate to refresh view with dt data source
            RefreshGridViewHabitsByDate(newRowDate);
        }

        // add a new habit
        private bool OpenAddHabitForm(string name, int row, string desc = "")
        {
            // save arguments as a tuple to pass to the AddHabitForm
            (int habitID, string name, string desc) habitData = (0, name, desc);

            // show AddHabitForm
            using (AddHabitForm addHabit = new AddHabitForm(curUserID, sqliteDb, curUserHabits, habitData))
            {
                // open form and get dialog result
                DialogResult result = addHabit.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // grab user input from AddHabitForm
                    int habitID = addHabit.UserHabitInput.habitID;
                    string habitName = addHabit.UserHabitInput.name;
                    string habitDescription = addHabit.UserHabitInput.description;

                    // create habit in db if it does not exist
                    if (habitID == 0)
                    {
                        Console.Write($"Creating new Habit record with: " +
                                      $"habit name: {habitName}, " +
                                      $"description: {habitDescription}.");
                        sqliteDb.CreateHabit(habitName, habitDescription, curUserID);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private int FindRowByHabitHasDateID(int habitHasDateID)
        {
            // TODO: need to change this method to search by habit name instead (if deleted habits_has_dates were modified before deleting, habitHasID searches break)
            int rowIndex = -1;

            // find row index where habitHasID matches 
            foreach (DataGridViewRow row in gridViewHabitsByDate.Rows)
            {
                if (row.Cells[habitHasDateIDCol].Value == null)
                {
                    rowIndex = 0;
                    break;
                }
                if (Convert.ToInt32(row.Cells[habitHasDateIDCol].Value.ToString()) == habitHasDateID)
                {
                    rowIndex = row.Index;
                    break;  // stop searching
                }
            }

            return rowIndex;  // TODO: will break if habitHasDateID not found
        }

        // commit row change to history and toggle undo/redo button text
        private void CommitChanges(string type, int row, string name, int quantity, string note, int habitHasDateID)
        {
            gridViewHabitsByDateHistory.Commit((type, row, name, quantity, note, habitHasDateID));
            btnUndo.ForeColor = SystemColors.ControlText;
            btnRedo.ForeColor = SystemColors.ControlDark;
        }
    }
}
