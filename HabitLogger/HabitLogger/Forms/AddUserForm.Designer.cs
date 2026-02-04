namespace HabitLogger
{
    partial class AddUserForm
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
            this.lblNewUser = new System.Windows.Forms.Label();
            this.txtNewUser = new System.Windows.Forms.TextBox();
            this.btnCreateNewUser = new System.Windows.Forms.Button();
            this.btnCloseNewUserForm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNewUser
            // 
            this.lblNewUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNewUser.AutoSize = true;
            this.lblNewUser.Location = new System.Drawing.Point(77, 69);
            this.lblNewUser.Name = "lblNewUser";
            this.lblNewUser.Size = new System.Drawing.Size(171, 20);
            this.lblNewUser.TabIndex = 0;
            this.lblNewUser.Text = "Enter New User Name:";
            // 
            // txtNewUser
            // 
            this.txtNewUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNewUser.Location = new System.Drawing.Point(74, 92);
            this.txtNewUser.Name = "txtNewUser";
            this.txtNewUser.Size = new System.Drawing.Size(214, 26);
            this.txtNewUser.TabIndex = 1;
            // 
            // btnCreateNewUser
            // 
            this.btnCreateNewUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCreateNewUser.Location = new System.Drawing.Point(107, 135);
            this.btnCreateNewUser.Name = "btnCreateNewUser";
            this.btnCreateNewUser.Size = new System.Drawing.Size(99, 37);
            this.btnCreateNewUser.TabIndex = 4;
            this.btnCreateNewUser.Text = "Add User";
            this.btnCreateNewUser.UseVisualStyleBackColor = true;
            this.btnCreateNewUser.Click += new System.EventHandler(this.btnCreateNewUser_Click);
            // 
            // btnCloseNewUserForm
            // 
            this.btnCloseNewUserForm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCloseNewUserForm.Location = new System.Drawing.Point(212, 135);
            this.btnCloseNewUserForm.Name = "btnCloseNewUserForm";
            this.btnCloseNewUserForm.Size = new System.Drawing.Size(76, 37);
            this.btnCloseNewUserForm.TabIndex = 5;
            this.btnCloseNewUserForm.Text = "Cancel";
            this.btnCloseNewUserForm.UseVisualStyleBackColor = true;
            this.btnCloseNewUserForm.Click += new System.EventHandler(this.btnCloseNewUserForm_Click);
            // 
            // NewUserForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(362, 266);
            this.Controls.Add(this.btnCloseNewUserForm);
            this.Controls.Add(this.btnCreateNewUser);
            this.Controls.Add(this.txtNewUser);
            this.Controls.Add(this.lblNewUser);
            this.Name = "NewUserForm";
            this.Text = "Add New User";
            this.Load += new System.EventHandler(this.NewUserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNewUser;
        private System.Windows.Forms.TextBox txtNewUser;
        private System.Windows.Forms.Button btnCreateNewUser;
        private System.Windows.Forms.Button btnCloseNewUserForm;
    }
}