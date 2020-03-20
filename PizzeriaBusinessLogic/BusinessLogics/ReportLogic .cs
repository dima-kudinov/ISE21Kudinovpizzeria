using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.HelperModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IIngredientLogic ingredientLogic;

        private readonly IPizzaLogic pizzaLogic;

        private readonly IOrderLogic orderLogic;

        public ReportLogic
            (IPizzaLogic pizzaLogic,
            IIngredientLogic ingredientLogic,
            IOrderLogic orderLLogic)
        {
            this.pizzaLogic = pizzaLogic;
            this.ingredientLogic = ingredientLogic;
            this.orderLogic = orderLLogic;
        }

        /// <summary>  
        /// /// Получение списка компонент с указанием, в каких изделиях используются    
        /// /// </summary>     
        /// /// <returns></returns>      
        public List<ReportPizzaIngViewModel> GetPizzaIng()
        {
            var ingredients = ingredientLogic.Read(null);

            var pizzas = pizzaLogic.Read(null);

            var list = new List<ReportPizzaIngViewModel>();

            foreach (var ingredient in ingredients)
            {
                var record = new ReportPizzaIngViewModel
                {
                    IngredientName = ingredient.IngredientName,
                    Pizzas = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var pizza in pizzas)
                {
                    if (pizza.PizzaIngs.ContainsKey(ingredient.Id))
                    {
                        record.Pizzas.Add(new Tuple<string, int>(pizza.PizzaName,
                            pizza.PizzaIngs[ingredient.Id].Item2));
                        record.TotalCount += pizza.PizzaIngs[ingredient.Id].Item2;
                    }
                }

                list.Add(record);
            }

            return list;
        }

        /// <summary>     
        /// /// Получение списка заказов за определенный период   
        /// /// </summary>     
        /// /// <param name="model"></param>   
        /// /// <returns></returns>     
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                PizzaName = x.PizzaName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status
            })
            .ToList();
        }

        /// <summary>   
        /// /// Сохранение компонент в файл-Word 
        /// /// </summary>        
        /// /// <param name="model"></param>    
        public void SaveIngredientsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,  
                Title = "Список компонент",
                Ingredients = ingredientLogic.Read(null)
                    });
        }

        /// <summary>    
        /// /// Сохранение компонент с указаеним продуктов в файл-Excel    
        /// /// </summary>     
        /// /// <param name="model"></param>     
        public void SavePizzaIngToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                PizzaIngs = GetPizzaIng()
            });
        }

        /// <summary>      
        /// /// Сохранение заказов в файл-Pdf    
        /// /// </summary>    
        /// /// <param name="model"></param>  
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}