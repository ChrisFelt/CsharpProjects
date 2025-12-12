using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HabitLogger
{
    public partial class AddHabitForm : Form
    {
        DbModel sqliteDb;
        int curUserID = 0;
        public AddHabitForm(int userID, DbModel db)
        {
            InitializeComponent();
            CenterToScreen();
            curUserID = userID;
            sqliteDb = db;
            // populate list view with this user's habits
            UpdateLstAddHabit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // todo: connect the habit with currently selected date
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateLstAddHabit()
        {
            List<(int habitID, string name, string description)> habitsLst = sqliteDb.ReadHabitByUser(curUserID);
            foreach (var habit in habitsLst)
            {
                // add items from the tuple as a row to lstAddHabit
                string[] items = new string[] {habit.name, habit.description, habit.habitID.ToString()};
                ListViewItem row = new ListViewItem(items);
                lstAddHabit.Items.Add(row);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // opens new window with 2 fields: 1) habit name and 2) description (optional)
            // new window contains 2 buttons: "Ok" and "Cancel"
            // "Ok" button: create new habit if input is validated successfully
            // "Cancel" button: close window and return to AddHabitForm
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // delete the currently selected habit from the list view and the database
            // a confirmation dialogue pops up as a new window to allow user to change their minds
        }
    }
}
