using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel> Read(IngredientBindingModel model);
        void CreateOrUpdate(IngredientBindingModel model);
        void Delete(IngredientBindingModel model);
    }
}
