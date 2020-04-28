using PizzeriaListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public List<PizzaIng> PizzaIngs { get; set; }
        private DataListSingleton()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<Order>();
            Pizzas = new List<Pizza>();
            PizzaIngs = new List<PizzaIng>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
