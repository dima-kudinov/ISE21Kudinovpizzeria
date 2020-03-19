using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaDatabaseImplement.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string PizzaName { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public virtual PizzaIng PizzaIng { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
