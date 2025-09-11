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
    public partial class main : Form
    {
        DbController sqliteDb = new DbController();
        int curUserID = 0;  // user not logged in
        public main()
        {
            InitializeComponent();
            CenterToScreen();
            // show only login panel when app is started
            pnlLogin.Show();
            pnlMain.Hide();
        }

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

                if (curUserID == 0)
                {
                    // login failed, notify user
                    MessageBox.Show($"Username: '{inputTxt}' does not exist.\nPlease try again.", "Login failed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Clear();
                    txtUserName.Select();
                }
                else
                {
                    // swap to main panel if login succeeds
                    pnlLogin.Hide();
                    pnlMain.Show();

                    // clear txtUserName for next login
                    txtUserName.Clear();
                    // TODO: welcome user by name in main panel
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // logout user and swap back to login panel
            pnlLogin.Show();
            pnlMain.Hide();
            curUserID = 0;
            txtUserName.Select();
        }

    }
}
