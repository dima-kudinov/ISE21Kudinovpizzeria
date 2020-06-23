﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PizzeriaDatabaseImplement.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [ForeignKey("IngredientId")]
        public virtual List<PizzaIng> PizzaIngs { get; set; }
        [ForeignKey("IngredientId")]
        public virtual List<StorageIngredient> StorageIngredients { get; set; }
    }
}
