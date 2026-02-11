using System;
using System.CodeDom;
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
    public partial class AddUserForm : Form
    {
        DbModel sqliteDb;
        public AddUserForm(DbModel db)
        {
            sqliteDb = db;
            InitializeComponent();
            CenterToScreen();
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
                // create new user, notify the user, and close NewUserForm
                inputTxt = inputTxt.Trim();

                // display success message and exit window when successful
                if (sqliteDb.CreateUser(inputTxt))
                {
                    MessageBox.Show($"New user: '{inputTxt}' added.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }

                // otherwise clear txtNewUser box and cancel operation
                else
                {
                    txtNewUser.Clear();
                    return;
                }
            }
        }

        private void btnCloseNewUserForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtNewUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            // fire btnCreateNewUser_Click event on Enter key press
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;  // suppress ding sound
                btnCreateNewUser.PerformClick();
            }
        }
    }
}
