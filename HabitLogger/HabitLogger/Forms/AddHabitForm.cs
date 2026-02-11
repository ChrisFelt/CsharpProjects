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
        (int habitID, string name, string description) initialHabitData;
        List<(int habitID, string name, string description)> habitsList;
        (int habitID, string name, string description) _userHabitInput;

        // allow main form to grab the user's input (read-only property)
        public (int habitID, string name, string description) UserHabitInput 
        {
            get { return _userHabitInput; }
        }

        public AddHabitForm(int userID, DbModel db, List<(int habitID, string name, string description)> curUserHabits, (int habitID, string name, string description) habitData)
        {
            InitializeComponent();
            CenterToScreen();
            curUserID = userID;
            sqliteDb = db;
            initialHabitData.habitID = habitData.habitID;
            initialHabitData.name = habitData.name;
            initialHabitData.description = habitData.description;

            // populate list of habits and display names in the combobox
            habitsList = GenerateHabitNameArray(curUserHabits);
            comboBoxHabitName.Items.AddRange(habitsList.Select(t=>t.name).ToArray());
            comboBoxHabitName.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // grab input from combobox and rtxt box and save to userHabitInput
            Console.WriteLine(comboBoxHabitName.SelectedIndex);
            _userHabitInput.habitID = habitsList[comboBoxHabitName.SelectedIndex].habitID;
            _userHabitInput.name = habitsList[comboBoxHabitName.SelectedIndex].name;
            _userHabitInput.description = richTextBoxHabitDesc.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void comboBoxHabitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBoxHabitDesc.Text = habitsList[comboBoxHabitName.SelectedIndex].description;
        }

        private void richTextBoxHabitDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // fire btnOk_Click event on Enter key press
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;  // suppress ding sound
                btnOk.PerformClick();
            }
        }

        private List<(int habitID, string name, string description)> GenerateHabitNameArray(List<(int habitID, string name, string description)> curUserHabits)
        {
            // populate return list with initial habit data and user's habits
            List<(int habitID, string name, string description)> returnList = new List<(int habitID, string name, string description)>(curUserHabits.Count + 1);

            returnList.Add(initialHabitData);  // this value will be the default value shown in the combobox

            foreach ((int habitID, string name, string description) habit in curUserHabits)
            {
                returnList.Add(habit);
                Console.WriteLine(habit);
            }

            return returnList;
        }
    }
}
