namespace PizzeriaClientView
{
    partial class FormUpdateData
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
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.labelClientFIO = new System.Windows.Forms.Label();
            this.textBoxClientFIO = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(20, 21);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(105, 20);
            this.labelEmail.TabIndex = 0;
            this.labelEmail.Text = "Логин(почта):";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(101, 18);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(200, 27);
            this.textBoxEmail.TabIndex = 1;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(20, 64);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(65, 20);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Пароль:";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(101, 61);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(200, 27);
            this.textBoxPassword.TabIndex = 3;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(149, 152);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(100, 36);
            this.buttonUpdate.TabIndex = 6;
            this.buttonUpdate.Text = "Сохранить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // labelClientFIO
            // 
            this.labelClientFIO.AutoSize = true;
            this.labelClientFIO.Location = new System.Drawing.Point(20, 108);
            this.labelClientFIO.Name = "labelClientFIO";
            this.labelClientFIO.Size = new System.Drawing.Size(45, 20);
            this.labelClientFIO.TabIndex = 4;
            this.labelClientFIO.Text = "ФИО:";
            // 
            // textBoxClientFIO
            // 
            this.textBoxClientFIO.Location = new System.Drawing.Point(101, 105);
            this.textBoxClientFIO.Name = "textBoxClientFIO";
            this.textBoxClientFIO.Size = new System.Drawing.Size(200, 27);
            this.textBoxClientFIO.TabIndex = 5;
            // 
            // FormUpdateData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 200);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.textBoxClientFIO);
            this.Controls.Add(this.labelClientFIO);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelEmail);
            this.Name = "FormUpdateData";
            this.Text = "Изменение данных";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelClientFIO;
        private System.Windows.Forms.TextBox textBoxClientFIO;
        private System.Windows.Forms.Button buttonUpdate;
    }
}