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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblNewUser_Click(object sender, EventArgs e)
        {
            // open NewUserForm window
            NewUserForm newUser = new NewUserForm(sqliteDb);
            newUser.Show();
        }

        // -----------------------------------------------------
        // pnlMain Events
        // -----------------------------------------------------

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // show AddHabitForm
            AddHabitForm addHabit = new AddHabitForm(curUserID, sqliteDb);
            addHabit.Show();
        }

        private void UpdateLstHabitsByDate(string date)
        {

            // populate LstHabitsByDate using ReadHabitsByDate()
            List<(int habitID, string name, string description, int habitHasDateID, string quantity)> habitsLst = sqliteDb.ReadHabitByDate(curUserID, date);
            foreach (var habit in habitsLst)
            {
                // add items from the tuple as a row to lstHabitsByDate
                string[] items = new string[] { habit.name, habit.quantity, habit.description, habit.habitID.ToString(), habit.habitHasDateID.ToString() };
                ListViewItem row = new ListViewItem(items);
                lstHabitsByDate.Items.Add(row);
            }
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            // first clear the list view
            lstHabitsByDate.Items.Clear();

            // then update the list view by invoking UpdateLstHabitsByDate() 
            UpdateLstHabitsByDate(e.Start.ToString("yyyy-MM-dd"));
            // TODO: There is currently no way to delete a date. Add delete date option?
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // open new window that allows the user to edit the current habit
        }

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
                    lstHabitsByDate.Items.Remove(habit);
                }
            }
        }
    }
}
