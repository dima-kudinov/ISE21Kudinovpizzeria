﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class StorageIngredientBindingModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int IngredientId { get; set; }
        public int Count { get; set; }
    }
}
