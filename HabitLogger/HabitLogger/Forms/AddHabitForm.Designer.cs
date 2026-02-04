namespace HabitLogger
{
    partial class AddHabitForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.lblHabitName = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.richTextBoxHabitDesc = new System.Windows.Forms.RichTextBox();
            this.lblHabitDesc = new System.Windows.Forms.Label();
            this.comboBoxHabitName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(71, 210);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(141, 38);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblHabitName
            // 
            this.lblHabitName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHabitName.AutoSize = true;
            this.lblHabitName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitName.Location = new System.Drawing.Point(38, 11);
            this.lblHabitName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHabitName.Name = "lblHabitName";
            this.lblHabitName.Size = new System.Drawing.Size(82, 16);
            this.lblHabitName.TabIndex = 10;
            this.lblHabitName.Text = "Habit Name:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(220, 210);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(141, 38);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // richTextBoxHabitDesc
            // 
            this.richTextBoxHabitDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richTextBoxHabitDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxHabitDesc.Location = new System.Drawing.Point(36, 94);
            this.richTextBoxHabitDesc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.richTextBoxHabitDesc.Name = "richTextBoxHabitDesc";
            this.richTextBoxHabitDesc.Size = new System.Drawing.Size(363, 97);
            this.richTextBoxHabitDesc.TabIndex = 12;
            this.richTextBoxHabitDesc.Text = "";
            // 
            // lblHabitDesc
            // 
            this.lblHabitDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHabitDesc.AutoSize = true;
            this.lblHabitDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHabitDesc.Location = new System.Drawing.Point(38, 71);
            this.lblHabitDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHabitDesc.Name = "lblHabitDesc";
            this.lblHabitDesc.Size = new System.Drawing.Size(78, 16);
            this.lblHabitDesc.TabIndex = 13;
            this.lblHabitDesc.Text = "Description:";
            // 
            // comboBoxHabitName
            // 
            this.comboBoxHabitName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxHabitName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxHabitName.FormattingEnabled = true;
            this.comboBoxHabitName.Location = new System.Drawing.Point(36, 34);
            this.comboBoxHabitName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxHabitName.Name = "comboBoxHabitName";
            this.comboBoxHabitName.Size = new System.Drawing.Size(363, 24);
            this.comboBoxHabitName.TabIndex = 11;
            this.comboBoxHabitName.SelectedIndexChanged += new System.EventHandler(this.comboBoxHabitName_SelectedIndexChanged);
            // 
            // AddHabitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 263);
            this.Controls.Add(this.lblHabitDesc);
            this.Controls.Add(this.richTextBoxHabitDesc);
            this.Controls.Add(this.comboBoxHabitName);
            this.Controls.Add(this.lblHabitName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddHabitForm";
            this.Text = "Add your habit!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblHabitName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox richTextBoxHabitDesc;
        private System.Windows.Forms.Label lblHabitDesc;
        private System.Windows.Forms.ComboBox comboBoxHabitName;
    }
}