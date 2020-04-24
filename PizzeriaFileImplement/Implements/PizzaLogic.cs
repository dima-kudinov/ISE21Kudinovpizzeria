using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class PizzaLogic : IPizzaLogic
    {
        private readonly FileDataListSingleton source;

        public PizzaLogic() {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(PizzaBindingModel model)
        {
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.PizzaName == model.PizzaName && rec.Id != model.Id);
            if (element != null) {
                throw new Exception("Уже есть изделие с таким названием");
            }

            if (model.Id.HasValue) {
                element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null) {
                    throw new Exception("Элемент не найден");
                }   
            }
            else
            {
                int maxId = source.Pizzas.Count > 0 ? source.Ingredients.Max(rec => rec.Id) : 0; element = new Pizza { Id = maxId + 1 };
                source.Pizzas.Add(element);
            }
            element.PizzaName = model.PizzaName;
            element.Price = model.Price;

            source.PizzaIngs.RemoveAll(rec => rec.PizzaId == model.Id && !model.PizzaIngs.ContainsKey(rec.IngredientId));             // обновили количество у существующих записей        
            var updateIngredients = source.PizzaIngs.Where(rec => rec.PizzaId == model.Id && model.PizzaIngs.ContainsKey(rec.IngredientId)); 

            foreach (var updateIngredient in updateIngredients)
            {
                updateIngredient.Count = model.PizzaIngs[updateIngredient.IngredientId].Item2;
                model.PizzaIngs.Remove(updateIngredient.IngredientId);
            }

            int maxPCId = source.PizzaIngs.Count > 0 ? source.PizzaIngs.Max(rec => rec.Id) : 0;

            foreach (var pc in model.PizzaIngs) { source.PizzaIngs.Add(new PizzaIng { Id = ++maxPCId, PizzaId = element.Id, IngredientId = pc.Key, Count = pc.Value.Item2 }); }
        }

        public void Delete(PizzaBindingModel model)
        {             // удаяем записи по компонентам при удалении изделия           
            source.PizzaIngs.RemoveAll(rec => rec.PizzaId == model.Id);
            Pizza element = source.Pizzas.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Pizzas.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        } 

            public List<PizzaViewModel> Read(PizzaBindingModel model) {
                return source.Pizzas.Where(rec => model == null || rec.Id == model.Id).Select(rec => new PizzaViewModel
                {
                    Id = rec.Id, PizzaName = rec.PizzaName,
                    Price = rec.Price,
                    PizzaIngs = source.PizzaIngs.Where(recPC => recPC.PizzaId == rec.Id).ToDictionary(recPC => recPC.IngredientId, recPC => (source.Ingredients.FirstOrDefault(recC => recC.Id == recPC.IngredientId)?.IngredientName, recPC.Count)) }).
                    ToList();
            }
        }
    } 