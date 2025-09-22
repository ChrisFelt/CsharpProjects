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
    public partial class NewUserForm : Form
    {
        DbModel sqliteDb;
        public NewUserForm(DbModel db)
        {
            sqliteDb = db;
            InitializeComponent();
            CenterToScreen();
        }

        private void NewUserForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCreateNewUser_Click(object sender, EventArgs e)
        {
            // validate input
            string inputTxt = txtNewUser.Text;
            if (string.IsNullOrWhiteSpace(inputTxt))
            {
                MessageBox.Show("Invalid Username.\nPlease enter one or more characters.", "Username Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // TODO: remove all whitespace?
                // create new user, notify the user, and close NewUserForm
                inputTxt = inputTxt.Trim(' ');
                sqliteDb.CreateUser(inputTxt);
                MessageBox.Show($"New user: '{inputTxt}' added.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void btnCloseNewUserForm_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
