using System;
using PizzeriaBusinessLogic.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class IngredientViewModel : BaseViewModel
    {
        [Column(title: "Ингредиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string IngredientName { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "IngredientName"
        };
    }
}
