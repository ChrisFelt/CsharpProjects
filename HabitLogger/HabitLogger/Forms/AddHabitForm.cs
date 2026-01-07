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
        public (int habitID, string name, string desc) initialHabitData;

        // allow main form to grab the user's input
        public (int habitID, string name, string desc) UserHabitInput { get; private set; }

        List<(int habitID, string name, string description)> habitsList;

        public AddHabitForm(int userID, DbModel db, List<(int habitID, string name, string description)> curUserHabits, (int habitID, string name, string desc) habitData)
        {
            InitializeComponent();
            CenterToScreen();
            curUserID = userID;
            sqliteDb = db;
            initialHabitData.habitID = habitData.habitID;
            initialHabitData.name = habitData.name;
            initialHabitData.desc = habitData.desc;

            // populate list of habits and display names in the combobox
            habitsList = GenerateHabitNameArray(curUserHabits);
            comboBoxHabitName.Items.AddRange(habitsList.Select(t=>t.name).ToArray());
            comboBoxHabitName.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // TODO: grab input from combobox and rtxt box and save to userHabitInput
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private List<(int habitID, string name, string description)> GenerateHabitNameArray(List<(int habitID, string name, string description)> curUserHabits)
        {
            // populate return list with initial habit data and user's habits
            List<(int habitID, string name, string description)> returnList = new List<(int habitID, string name, string description)>(curUserHabits.Count + 1);

            returnList.Add(initialHabitData);  // this value will be the default value shown in the combobox

            foreach ((int habitID, string name, string description) habit in curUserHabits)
            {
                returnList.Add(habit);
            }

            return returnList;
        }
    }
}
