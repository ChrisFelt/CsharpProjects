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
        DbController sqliteDb;
        public NewUserForm(DbController db)
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
                sqliteDb.CreateUser(inputTxt.Trim(' '));
            }
        }

        private void btnCloseNewUserForm_Click(object sender, EventArgs e)
        {
            // temporary: read Users on click
            string inputTxt = txtNewUser.Text;
            inputTxt = inputTxt.Trim(' ');
            MessageBox.Show($"User ID for {inputTxt}: {sqliteDb.ReadUser(inputTxt)}", "Username Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
