﻿using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.BusinessLogics;
using PizzeriaBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace PizzeriaView
{
    public partial class FormFillStorage : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IIngredientLogic logicF;
        private readonly MainLogic logicM;
        private readonly IStorageLogic logicS;
        public FormFillStorage(IIngredientLogic logicF, MainLogic logicM, IStorageLogic logicS)
        {
            InitializeComponent();
            this.logicF = logicF;
            this.logicM = logicM;
            this.logicS = logicS;
        }
        private void FormFillStorage_Load(object sender, EventArgs e)
        {
            try
            {
                var storageList = logicS.GetList();
                comboBoxStorage.DataSource = storageList;
                comboBoxStorage.DisplayMember = "StorageName";
                comboBoxStorage.ValueMember = "Id";

                var ingredientList = logicF.Read(null);
                comboBoxIngredient.DataSource = ingredientList;
                comboBoxIngredient.DisplayMember = "IngredientName";
                comboBoxIngredient.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните количество", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                return;
            }
            if (comboBoxIngredient.SelectedValue == null)
            {
                MessageBox.Show("Выберите ингредиент", "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                return;
            }

            try
            {
                int storageId = Convert.ToInt32(comboBoxStorage.SelectedValue);
                int ingredientId = Convert.ToInt32(comboBoxIngredient.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);

                this.logicM.FillStorage(new StorageIngredientBindingModel
                {
                    StorageId = storageId,
                    IngredientId = ingredientId,
                    Count = count
                });
                MessageBox.Show("Склад успешно пополнен", "Сообщение",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}