using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PizzeriaDatabaseImplement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaDatabaseImplement 
{
    class PizzeriaDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8TG86ND\SQLEXPRESS;Initial Catalog=PizzeriaDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }  
 
        public virtual DbSet<Ingredient> Ingredients { set; get; }

        public virtual DbSet<Pizza> Pizzas { set; get; }

        public virtual DbSet<PizzaIng> PizzaIng{ set; get; }

        public virtual DbSet<Order> Orders { set; get; }
    }
}
