namespace HabitLogger
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.gridViewHabitsByDate = new System.Windows.Forms.DataGridView();
            this.lblDisplayUser = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblGreet = new System.Windows.Forms.Label();
            this.lblHabitDesc = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.rtxtHabitDesc = new System.Windows.Forms.RichTextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblSelectHabit = new System.Windows.Forms.Label();
            this.lblSelectDate = new System.Windows.Forms.Label();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstHabitsByDate = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblNewUser = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.Habit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frequency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHabitsByDate)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlMain.Controls.Add(this.gridViewHabitsByDate);
            this.pnlMain.Controls.Add(this.lblDisplayUser);
            this.pnlMain.Controls.Add(this.btnLogout);
            this.pnlMain.Controls.Add(this.lblGreet);
            this.pnlMain.Controls.Add(this.lblHabitDesc);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Controls.Add(this.btnEdit);
            this.pnlMain.Controls.Add(this.rtxtHabitDesc);
            this.pnlMain.Controls.Add(this.btnAdd);
            this.pnlMain.Controls.Add(this.lblSelectHabit);
            this.pnlMain.Controls.Add(this.lblSelectDate);
            this.pnlMain.Controls.Add(this.monthCalendar);
            this.pnlMain.Controls.Add(this.menuStrip);
            this.pnlMain.Controls.Add(this.lstHabitsByDate);
            this.pnlMain.Location = new System.Drawing.Point(8, 20);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(816, 489);
            this.pnlMain.TabIndex = 0;
            // 
            // gridViewHabitsByDate
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewHabitsByDate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridViewHabitsByDate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewHabitsByDate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Habit,
            this.Frequency,
            this.Note});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridViewHabitsByDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridViewHabitsByDate.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.gridViewHabitsByDate.Location = new System.Drawing.Point(297, 70);
            this.gridViewHabitsByDate.Name = "gridViewHabitsByDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewHabitsByDate.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridViewHabitsByDate.RowHeadersWidth = 25;
            this.gridViewHabitsByDate.RowTemplate.Height = 28;
            this.gridViewHabitsByDate.Size = new System.Drawing.Size(492, 105);
            this.gridViewHabitsByDate.TabIndex = 14;
            this.gridViewHabitsByDate.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewHabitsByDate_CellContentClick);
            this.gridViewHabitsByDate.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridViewHabitsByDate_CellBeginEdit);
            this.gridViewHabitsByDate.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewHabitsByDate_CellEndEdit);
            this.gridViewHabitsByDate.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridViewHabitsByDate_DataError);
            this.gridViewHabitsByDate.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.gridViewHabitsByDate_UserDeletingRow);
            this.gridViewHabitsByDate.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridViewHabitsByDate_EditingControlShowing);
            this.gridViewHabitsByDate.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridViewHabitsByDate_UserAddedRow);
            this.gridViewHabitsByDate.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewHabitsByDate_CellValueChanged);
            // 
            // lblDisplayUser
            // 
            this.lblDisplayUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayUser.Location = new System.Drawing.Point(25, 284);
            this.lblDisplayUser.Name = "lblDisplayUser";
            this.lblDisplayUser.Size = new System.Drawing.Size(227, 31);
            this.lblDisplayUser.TabIndex = 12;
            this.lblDisplayUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogout.Location = new System.Drawing.Point(77, 354);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(124, 55);
            this.btnLogout.TabIndex = 11;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblGreet
            // 
            this.lblGreet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblGreet.AutoSize = true;
            this.lblGreet.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreet.Location = new System.Drawing.Point(69, 249);
            this.lblGreet.Name = "lblGreet";
            this.lblGreet.Size = new System.Drawing.Size(142, 31);
            this.lblGreet.TabIndex = 10;
            this.lblGreet.Text = "Welcome,";
            // 
            // lblHabitDesc
            // 
            this.lblHabitDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHabitDesc.AutoSize = true;
            this.lblHabitDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitDesc.Location = new System.Drawing.Point(303, 345);
            this.lblHabitDesc.Name = "lblHabitDesc";
            this.lblHabitDesc.Size = new System.Drawing.Size(111, 16);
            this.lblHabitDesc.TabIndex = 9;
            this.lblHabitDesc.Text = "Habit description:";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.Location = new System.Drawing.Point(713, 299);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(76, 30);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEdit.Location = new System.Drawing.Point(631, 299);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(76, 30);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // rtxtHabitDesc
            // 
            this.rtxtHabitDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rtxtHabitDesc.Location = new System.Drawing.Point(300, 368);
            this.rtxtHabitDesc.Name = "rtxtHabitDesc";
            this.rtxtHabitDesc.ReadOnly = true;
            this.rtxtHabitDesc.Size = new System.Drawing.Size(489, 99);
            this.rtxtHabitDesc.TabIndex = 6;
            this.rtxtHabitDesc.Text = "";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.Location = new System.Drawing.Point(549, 299);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(76, 30);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblSelectHabit
            // 
            this.lblSelectHabit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSelectHabit.AutoSize = true;
            this.lblSelectHabit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectHabit.Location = new System.Drawing.Point(299, 47);
            this.lblSelectHabit.Name = "lblSelectHabit";
            this.lblSelectHabit.Size = new System.Drawing.Size(91, 16);
            this.lblSelectHabit.TabIndex = 4;
            this.lblSelectHabit.Text = "Select a habit:";
            // 
            // lblSelectDate
            // 
            this.lblSelectDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSelectDate.AutoSize = true;
            this.lblSelectDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectDate.Location = new System.Drawing.Point(23, 47);
            this.lblSelectDate.Name = "lblSelectDate";
            this.lblSelectDate.Size = new System.Drawing.Size(89, 16);
            this.lblSelectDate.TabIndex = 3;
            this.lblSelectDate.Text = "Select a date:";
            // 
            // monthCalendar
            // 
            this.monthCalendar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.monthCalendar.Location = new System.Drawing.Point(26, 70);
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.TabIndex = 1;
            this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(14, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(52, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // lstHabitsByDate
            // 
            this.lstHabitsByDate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstHabitsByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHabitsByDate.FullRowSelect = true;
            this.lstHabitsByDate.HideSelection = false;
            this.lstHabitsByDate.Location = new System.Drawing.Point(300, 181);
            this.lstHabitsByDate.Name = "lstHabitsByDate";
            this.lstHabitsByDate.Size = new System.Drawing.Size(489, 113);
            this.lstHabitsByDate.TabIndex = 13;
            this.lstHabitsByDate.UseCompatibleStateImageBehavior = false;
            this.lstHabitsByDate.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Frequency";
            this.columnHeader2.Width = 76;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Note";
            this.columnHeader3.Width = 289;
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserName.Location = new System.Drawing.Point(298, 240);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(236, 26);
            this.txtUserName.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(241, 127);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(350, 31);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Welcome to Habit Logger!";
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogin.Location = new System.Drawing.Point(376, 283);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(76, 37);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.Location = new System.Drawing.Point(459, 283);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(76, 37);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(301, 218);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(93, 20);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "User Name:";
            // 
            // lblNewUser
            // 
            this.lblNewUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNewUser.AutoSize = true;
            this.lblNewUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewUser.Location = new System.Drawing.Point(294, 354);
            this.lblNewUser.Name = "lblNewUser";
            this.lblNewUser.Size = new System.Drawing.Size(244, 16);
            this.lblNewUser.TabIndex = 6;
            this.lblNewUser.Text = "New User? Click here to create a profile!";
            this.lblNewUser.Click += new System.EventHandler(this.lblNewUser_Click);
            // 
            // pnlLogin
            // 
            this.pnlLogin.Controls.Add(this.lblNewUser);
            this.pnlLogin.Controls.Add(this.lblUserName);
            this.pnlLogin.Controls.Add(this.btnExit);
            this.pnlLogin.Controls.Add(this.btnLogin);
            this.pnlLogin.Controls.Add(this.lblWelcome);
            this.pnlLogin.Controls.Add(this.txtUserName);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(0, 0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(832, 528);
            this.pnlLogin.TabIndex = 0;
            // 
            // Habit
            // 
            this.Habit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Habit.DataPropertyName = "Habit";
            this.Habit.HeaderText = "Habit";
            this.Habit.Name = "Habit";
            // 
            // Frequency
            // 
            this.Frequency.DataPropertyName = "Frequency";
            this.Frequency.HeaderText = "Frequency";
            this.Frequency.Name = "Frequency";
            this.Frequency.Width = 109;
            // 
            // Note
            // 
            this.Note.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Note.DataPropertyName = "Note";
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            // 
            // main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(832, 528);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "main";
            this.Text = "Habit Logger";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewHabitsByDate)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblNewUser;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Label lblSelectDate;
        private System.Windows.Forms.Label lblSelectHabit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblHabitDesc;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.RichTextBox rtxtHabitDesc;
        private System.Windows.Forms.Label lblGreet;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblDisplayUser;
        private System.Windows.Forms.ListView lstHabitsByDate;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.DataGridView gridViewHabitsByDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Habit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Frequency;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
    }
}

