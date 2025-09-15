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
        DbController sqliteDb;
        int curUserID = 0;
        public AddHabitForm(int userID, DbController db)
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

        }

        private void UpdateLstAddHabit()
        {
            List<(int habitID, string name, string description)> habitsLst = sqliteDb.ReadHabitByUser(curUserID);
            foreach (var habit in habitsLst)
            {
                // add items from the tuple as a row to lstAddHabit
                string[] items = new string[] {habit.name, habit.description};
                ListViewItem row = new ListViewItem(items);
                lstAddHabit.Items.Add(row);
            }
        }
    }
}
