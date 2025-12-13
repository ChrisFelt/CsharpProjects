using System;
using System.Collections.Generic;
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
        DbModel sqliteDb = new DbModel();
        int curUserID = 0;  // user not logged in
        int indexHabitHasDateID = 4;  // ListViewItem index for habitHasDateID
        public main()
        {
            InitializeComponent();
            CenterToScreen();
            // show only login panel when app is started
            pnlLogin.Show();
            pnlMain.Hide();
        }

        // -----------------------------------------------------
        // pnlLogin Events
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

                if (curUserID > 0)
                {
                    // login success, swap to pnlMain
                    pnlLogin.Hide();
                    pnlMain.Show();

                    // clear txtUserName for next login
                    txtUserName.Clear();
                    // display current username on pnlMain
                    lblDisplayUser.Text = inputTxt;
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

        // DataGridView Cell click event
        private void gridViewHabitsByDate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // TODO: update Description to the selected habit
        }

        // TODO:
        // add undoHistory and redoHistory LIFO stack variables
        // add text beneath DataGridView that is invisible until a change is made, then clickable "undo" is displayed
        // Cell value changed event in DataGridView for gridViewHabitsByDate
        // see: https://stackoverflow.com/questions/19537784/datagridview-event-to-catch-when-cell-value-has-been-changed-by-user
        // call UpdateHabitHasDate with the new values
        // add action to undoHistory and set undo text to visible and bring to front (set redo text to invisible and push to back)
        // gridViewHabitsByDate will NOT allow creation of new rows - new habits will be added by double clicking habits in the new DataGridView below

        // TODO:
        // add overlapping Undo and Redo text under gridViewHabitsByDate, set to invisible by default
        // add separate click events to both texts
        // Undo click event:
        // 1. pops top value off of undoHistory and replaces the current value of that cell with the undoHistory value
        // 2. pushes the value into redoHistory
        // Redo click event does the reverse

        // TODO: 
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
        private void UpdateGridHabitsByDate(string date)
        {
            // get populated DataTable from db for this date
            gridViewHabitsByDate.DataSource = sqliteDb.ReadHabitByDateDT(curUserID, date);

            // hide IDs and description
            gridViewHabitsByDate.Columns["habitID"].Visible = false;
            gridViewHabitsByDate.Columns["Description"].Visible = false;
            gridViewHabitsByDate.Columns["habitHasDateID"].Visible = false;
        }
    }
}
