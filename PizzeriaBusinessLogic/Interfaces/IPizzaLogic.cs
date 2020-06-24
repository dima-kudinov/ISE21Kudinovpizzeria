using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface IPizzaLogic
    {
        List<PizzaViewModel> Read(PizzaBindingModel model);
        void CreateOrUpdate(PizzaBindingModel model);
        void Delete(PizzaBindingModel model);
    }
}
