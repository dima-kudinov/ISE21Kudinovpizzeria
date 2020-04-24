using PizzeriaBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BindingModels;

namespace PizzeriaView
{
    public partial class FormPizza : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IPizzaLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> PizzaIngs;
        public FormPizza(IPizzaLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }
        private void FormPizza_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PizzaViewModel view = logic.Read(new PizzaBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.PizzaName;
                        textBoxPrice.Text = view.Price.ToString();
                        PizzaIngs = view.PizzaIngs;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                PizzaIngs = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (PizzaIngs != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var bf in PizzaIngs)
                    {
                        dataGridView.Rows.Add(new object[] { bf.Key, bf.Value.Item1, bf.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormPizzaIng>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (PizzaIngs.ContainsKey(form.Id))
                {
                    PizzaIngs[form.Id] = (form.IngredientName, form.Count);
                }
                else
                {
                    PizzaIngs.Add(form.Id, (form.IngredientName, form.Count));
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormPizzaIng>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = PizzaIngs[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    PizzaIngs[form.Id] = (form.IngredientName, form.Count);
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        PizzaIngs.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (PizzaIngs == null || PizzaIngs.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new PizzaBindingModel
                {
                    Id = id,
                    PizzaName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    PizzaIngs = PizzaIngs
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
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
