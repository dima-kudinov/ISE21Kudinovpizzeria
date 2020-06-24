using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaListImplement.Models
{
    public class PizzaIng
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int IngredientId { get; set; }
        public int Count { get; set; }
    }
}
