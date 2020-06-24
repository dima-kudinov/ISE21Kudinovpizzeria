using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class PizzaViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название пиццы")]
        public string PizzaName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> PizzaIngs { get; set; }
    }
}
