﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }
}
