using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
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
        private int indexHabitHasDateID = 4;  // ListViewItem index for habitHasDateID
        private string prevCellContents;  // track contents of a cell before edit

        // data source for gridViewHabitsByDate
        private DataTable dt;

        // gridViewHabitsByDate columns
        private int habitNameCol = 0;
        private int quantityCol = 1;
        private int noteCol = 2;
        private int habitHasDateIDCol = 4;

        // track gridViewHabitsByDate row/cell history separately
        private DataGridViewHistory gridViewHabitsByDateHistory = new DataGridViewHistory();

        // hard code history types
        private string cellType = "cell";
        private string rowType = "row";

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
                    UpdateGridHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));
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
            // TODO: populate lstHabitsByDate with today's date
        }

        // Add button click event
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // show AddHabitForm
            AddHabitForm addHabit = new AddHabitForm(curUserID, sqliteDb);
            addHabit.Show();
        }

        // Date click event
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            // first clear the list view
            lstHabitsByDate.Items.Clear();

            // then update the list view by invoking UpdateLstHabitsByDate() 
            UpdateLstHabitsByDate(e.Start.ToString("yyyy-MM-dd"));
            UpdateGridHabitsByDate(e.Start.ToString("yyyy-MM-dd"));
            // TODO: There is currently no way to delete a date. Add delete date option?
        }

        // Edit button click event
        private void btnEdit_Click(object sender, EventArgs e)
        {
            // open new window that allows the user to edit the current habit
            dt.Rows[0][1] = "New value";
        }

        // Delete button click event
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // delete the currently selected habit from the current date ONLY
            // confirmation popup allows user to change their mind
            DialogResult confirm = MessageBox.Show("Remove habit from this date permanently?", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                // delete habitHasDate for each item selected in the listview
                foreach (ListViewItem habit in lstHabitsByDate.SelectedItems)
                {
                    Console.WriteLine($"Deleting ID: {int.Parse(habit.SubItems[indexHabitHasDateID].Text)}");
                    sqliteDb.DeleteHabitHasDate(int.Parse(habit.SubItems[indexHabitHasDateID].Text));

                    // remove habit from lstHabitsByDate display
                    // TODO: test with large selection of habits
                    lstHabitsByDate.Items.Remove(habit);
                }
            }
        }

        // -----------------------------------------------------
        // pnlMain gridViewHabitsByDate Events
        // -----------------------------------------------------
        // DataGridView Cell click event
        private void gridViewHabitsByDate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: update Description to the selected habit
            //Console.WriteLine("Cell content click event handler called.");
        }

        // grab contents of the cell before user edits it
        private void gridViewHabitsByDate_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Console.WriteLine($"Editing cell at row {e.RowIndex} and column {e.ColumnIndex}");
            prevCellContents = gridViewHabitsByDate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            Console.WriteLine($"Contents: {prevCellContents}");
        }

        // validate edits made to a cell and update DataGridViewHistory
        private void gridViewHabitsByDate_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 1. add new row routine - check if cell is on new row
            if (e.RowIndex == dt.Rows.Count)
            {
                // exit event without commiting data to db if no habit name entered
                if (gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitNameCol].Value.ToString() != "")
                {
                    gridViewHabitsByDate.Columns[habitNameCol].ValueType = typeof(string);
                    string newRowHabit = gridViewHabitsByDate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    // if habit name does not exist, prompt user to create new habit
                    if (curUserHabits.Any(tuple => tuple.name == newRowHabit))
                    {

                        // grab cell values for the row besides name
                        int newRowQuantity = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[quantityCol].Value.ToString());
                        string newRowNote = gridViewHabitsByDate.Rows[e.RowIndex].Cells[noteCol].Value.ToString();
                        string newRowDate = monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd");

                        // call CreateDate (method only adds date if it doesn't already exist)
                        sqliteDb.CreateDate(newRowDate);

                        // create new Habits_Has_Dates record
                        Console.Write($"Creating new HabitHasDate record with: " +
                                          $"habit name: {newRowHabit}, " +
                                          $"quantity: {newRowQuantity}, " +
                                          $"note: {newRowNote}, " +
                                          $"date: {newRowDate}... ");
                        sqliteDb.CreateHabitHasDate(newRowHabit,
                                                    newRowQuantity,
                                                    newRowNote,
                                                    newRowDate);
                        Console.WriteLine("Success!");

                        // call UpdateGridHabitsByDate to refresh view with dt data source
                        UpdateGridHabitsByDate(monthCalendar.SelectionRange.Start.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        // popup asks user if they want to add new habit
                        // if yes, open AddHabitForm
                        // if no, delete row
                    }
                }
                else
                {
                    Console.WriteLine("New row routine aborted due to empty habit name column value.");
                    // TODO: notify user that they must enter a habit name for row to be saved?
                    // TODO: either delete this row here, or disallow user from creating a new row until this row is added to db
                    // gridViewHabitsByDate.AllowUserToAddRows = false;  // causes infinite loop
                    gridViewHabitsByDate.Rows.Remove(gridViewHabitsByDate.Rows[e.RowIndex]);
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
                if (e.ColumnIndex == habitNameCol)
                {
                    gridViewHabitsByDate.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = prevCellContents;
                }

                // commit all valid edits to db and update history (note: all values are valid for note column)
                else if (prevCellContents != curCellContents)
                {
                    // TODO: do not write to db unless both habit name and quantity columns have a value
                    WriteRowToDb(e.RowIndex);
                    // commit change to history
                    int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[quantityCol].Value);
                    string note = gridViewHabitsByDate.Rows[e.RowIndex].Cells[noteCol].Value.ToString();
                    int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[e.RowIndex].Cells[habitHasDateIDCol].Value);

                    gridViewHabitsByDateHistory.Commit((cellType, e.RowIndex, quantity, note, habitHasDateID));
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
            string name = gridViewHabitsByDate.Rows[e.Row.Index].Cells[habitNameCol].Value.ToString();
            int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[e.Row.Index].Cells[quantityCol].Value);
            string note = gridViewHabitsByDate.Rows[e.Row.Index].Cells[noteCol].Value.ToString();
            int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[e.Row.Index].Cells[habitHasDateIDCol].Value);
            Console.WriteLine($"User deleting row: {e.Row.Index} with contents: {name} {quantity} {note} {habitHasDateID}.");
            // NOTE: this event fires for EACH row deleted. Multiple rows deleted simultaneously each cause the event to fire individually.
            // Need to track how many rows were deleted with history. Add new class variable in DataGridViewHistory to count?
            gridViewHabitsByDateHistory.Commit((rowType, e.Row.Index, quantity, note, habitHasDateID));
            gridViewHabitsByDateHistory.DeletedRowsCount++;
            Console.WriteLine($"Deleted row count: {gridViewHabitsByDateHistory.DeletedRowsCount}");

            // delete row from db
            DeleteRow(habitHasDateID);
        }

        // autocomplete for new habit in habit name column of gridViewHabitsByDate
        private void gridViewHabitsByDate_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            // enable autocomplete for new rows in the habit name column
            if (gridViewHabitsByDate.CurrentCell.ColumnIndex == habitNameCol && gridViewHabitsByDate.CurrentCell.RowIndex == gridViewHabitsByDate.Rows.Count - 1)
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

        // TODO:
        // add Undo and Redo buttons (replace Add/Edit?) under gridViewHabitsByDate
        // add separate click events to both buttons
        // Undo click event:
        // 1. pops top value off of undoHistory and replaces the current value of that cell with the undoHistory value
        // 2. pushes the value into redoHistory
        // Redo click event does the reverse

        // TODO: 
        // refresh curUserHabits when a new habit record is added
        // replace rtxtHabitDesc with DataGridView of all existing habits
        // allow double-click event to add current selection to the DataGridView as a new row
        // update whenever a new habit is created
        // allow creation of new habit within the DataGriView, and editing the habit and its description
        // use RowLeave event to strictly control edits, with confirmation popups each time a cell is edited

        // -----------------------------------------------------
        // pnlMain General Use Methods
        // -----------------------------------------------------
        // refresh ListView
        private void UpdateLstHabitsByDate(string date)
        {

            // populate LstHabitsByDate using ReadHabitsByDate()
            List<(int habitID, string name, string note, int habitHasDateID, string quantity)> habitsLst = sqliteDb.ReadHabitByDate(curUserID, date);
            foreach (var habit in habitsLst)
            {
                // add items from the tuple as a row to lstHabitsByDate
                string[] items = new string[] { habit.name, habit.quantity, habit.note, habit.habitID.ToString(), habit.habitHasDateID.ToString() };
                ListViewItem row = new ListViewItem(items);
                lstHabitsByDate.Items.Add(row);
            }
        }

        // refresh DataGridView
        // TODO: change name to RefreshGridHabitsByDate?
        private void UpdateGridHabitsByDate(string date)
        {
            // get populated DataTable from db for this date
            dt = sqliteDb.ReadHabitByDateDT(curUserID, date);

            gridViewHabitsByDate.DataSource = dt;

            // hide IDs and description
            gridViewHabitsByDate.Columns["habitID"].Visible = false;
            gridViewHabitsByDate.Columns["Description"].Visible = false;
            gridViewHabitsByDate.Columns["habitHasDateID"].Visible = false;

            // TODO: allow user to enter habit name in new row under name column, then query db for habit by that name (not case-specific)
            // if habit exists, generate new row automatically with the habit and a frequency of 0
            // if it does not exist, delete text in the new row and notify user with popup asking if they want to create the habit

        }

        // call UpdateHabitHasDate on a given DataGridView row
        private void WriteRowToDb(int row)
        {
            // get values from the given row
            string note = gridViewHabitsByDate.Rows[row].Cells[noteCol].Value.ToString();
            int quantity = Convert.ToInt32(gridViewHabitsByDate.Rows[row].Cells[quantityCol].Value.ToString());
            int habitHasDateID = Convert.ToInt32(gridViewHabitsByDate.Rows[row].Cells[habitHasDateIDCol].Value.ToString());

            // update db
            Console.Write($"Attempting to write note: {note}, quantity: {quantity}, habitHasDateID: {habitHasDateID}... ");
            sqliteDb.UpdateHabitHasDate(note, quantity, habitHasDateID);
            Console.WriteLine("Success!");
        }

        // delete row from HabitsHasDates
        private void DeleteRow(int habitHasDateID)
        {
            sqliteDb.DeleteHabitHasDate(habitHasDateID);
            Console.WriteLine($"Successfully deleted HabitsHasDates row with ID: {habitHasDateID}.");
        }
    }
}
