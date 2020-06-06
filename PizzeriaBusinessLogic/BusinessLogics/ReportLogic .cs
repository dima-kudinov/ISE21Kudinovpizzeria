using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.HelperModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IIngredientLogic IngredientLogic;
        private readonly IPizzaLogic PizzaLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IStorageLogic storageLogic;
        public ReportLogic(IPizzaLogic PizzaLogic, IIngredientLogic IngredientLogic,
       IOrderLogic orderLogic, IStorageLogic storageLogic)
        {
            this.PizzaLogic = PizzaLogic;
            this.IngredientLogic = IngredientLogic;
            this.orderLogic = orderLogic;
            this.storageLogic = storageLogic;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportPizzaIngViewModel> GetPizzaIng()
        {          
            var Pizzas = PizzaLogic.Read(null);
            var list = new List<ReportPizzaIngViewModel>();
            foreach (var pizza in Pizzas)
            {
                foreach (var pi in pizza.PizzaIngs)
                {
                    var record = new ReportPizzaIngViewModel
                    {
                        PizzaName = pizza.PizzaName,
                        IngredientName = pi.Value.Item1,
                        Count = pi.Value.Item2
                    };
                    list.Add(record);
                }
                
            }
            return list;
        }
        public List<ReportStorageIngredientViewModel> GetStorageIngredients()
        {
            var list = new List<ReportStorageIngredientViewModel>();
            var storages = storageLogic.GetList();
            foreach (var storage in storages)
            {
                foreach (var sf in storage.StorageIngredients)
                {
                    var record = new ReportStorageIngredientViewModel
                    {
                        StorageName = storage.StorageName,
                        IngredientName = sf.IngredientName,
                        Count = sf.Count
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SavePizzasToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список пицц",
                Pizzas = PizzaLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение закусок с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }
        /// <summary>
        /// Сохранение закусок с продуктами в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        public void SavePizzasToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список пицц по ингредиентам",
                PizzaIngs = GetPizzaIng(),
            });
        }
        public void SaveStoragesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Storages = storageLogic.GetList()
            });
        }
        public void SaveStorageIngredientsToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список продуктов в складах",
                Storages = storageLogic.GetList()
            });
        }
        public void SaveStorageIngredientsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список продуктов",
                StorageIngredients = GetStorageIngredients()
            });
        }
    }
}