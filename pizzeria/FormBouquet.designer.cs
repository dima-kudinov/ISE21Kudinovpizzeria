using System.Windows.Forms;

namespace Pizzeria
{
    partial class FormBouquet
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpd = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonRef = new System.Windows.Forms.Button();
            this.labelFlowers = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView.Location = new System.Drawing.Point(94, 162);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(546, 449);
            this.dataGridView.TabIndex = 2;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(689, 187);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(173, 41);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpd
            // 
            this.buttonUpd.Location = new System.Drawing.Point(689, 256);
            this.buttonUpd.Name = "buttonUpd";
            this.buttonUpd.Size = new System.Drawing.Size(173, 41);
            this.buttonUpd.TabIndex = 5;
            this.buttonUpd.Text = "Изменить";
            this.buttonUpd.UseVisualStyleBackColor = true;
            this.buttonUpd.Click += new System.EventHandler(this.buttonUpd_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(689, 327);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(173, 41);
            this.buttonDel.TabIndex = 4;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // buttonRef
            // 
            this.buttonRef.Location = new System.Drawing.Point(689, 397);
            this.buttonRef.Name = "buttonRef";
            this.buttonRef.Size = new System.Drawing.Size(173, 41);
            this.buttonRef.TabIndex = 3;
            this.buttonRef.Text = "Обновить";
            this.buttonRef.UseVisualStyleBackColor = true;
            this.buttonRef.Click += new System.EventHandler(this.buttonRef_Click);
            // 
            // labelFlowers
            // 
            this.labelFlowers.AutoSize = true;
            this.labelFlowers.Location = new System.Drawing.Point(96, 133);
            this.labelFlowers.Name = "labelFlowers";
            this.labelFlowers.Size = new System.Drawing.Size(59, 20);
            this.labelFlowers.TabIndex = 7;
            this.labelFlowers.Text = "Цветы";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(705, 642);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(157, 56);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(522, 642);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(157, 56);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(192, 31);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(83, 20);
            this.labelName.TabIndex = 9;
            this.labelName.Text = "Название";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(281, 31);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(359, 26);
            this.textBoxName.TabIndex = 8;
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(192, 85);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(48, 20);
            this.labelPrice.TabIndex = 13;
            this.labelPrice.Text = "Цена";
            // 
            // textBoxPrice
            // 
            this.textBoxPrice.Location = new System.Drawing.Point(281, 82);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.Size = new System.Drawing.Size(359, 26);
            this.textBoxPrice.TabIndex = 12;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Id";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Компонент";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Количество";
            this.Column3.Name = "Column3";
            this.Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.AllowUserToAddRows = false;

            // 
            // FormBouquet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 724);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.textBoxPrice);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelFlowers);
            this.Controls.Add(this.buttonRef);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpd);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormBouquet";
            this.Text = "Композиция";
            this.Load += new System.EventHandler(this.FormBouquet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpd;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonRef;
        private System.Windows.Forms.Label labelFlowers;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}