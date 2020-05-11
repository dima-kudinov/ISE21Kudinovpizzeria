﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.BindingModels
{
    public class StorageBindingModel
    {
        public int Id { get; set; }
        public string StorageName { get; set; }
        public List<StorageIngredientBindingModel> StorageIngredients { get; set; }
    }
}
