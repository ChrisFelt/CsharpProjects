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
        int curUserID;
        string habitName;
        string habitDesc;

        public AddHabitForm(int userID, DbModel db, (string name, string desc) habitData)
        {
            InitializeComponent();
            CenterToScreen();
            curUserID = userID;
            sqliteDb = db;
            habitName = habitData.name;
            habitDesc = habitData.desc;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // todo: connect the habit with currently selected date
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
