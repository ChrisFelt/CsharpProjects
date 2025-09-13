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
            curUserID = userID;
            sqliteDb = db;
            // populate list view with this user's habits

        }

    }
}
