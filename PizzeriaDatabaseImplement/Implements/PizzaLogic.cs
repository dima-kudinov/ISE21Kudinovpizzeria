using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaDatabaseImplement.Implements
{
    public class PizzaLogic : IPizzaLogic
    {
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Pizza element = context.Pizzas.FirstOrDefault(rec =>
                       rec.PizzaName == model.PizzaName && rec.Id != model.Id);

                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Pizzas.FirstOrDefault(rec => rec.Id ==
                           model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Pizza();
                            context.Pizzas.Add(element);
                        }
                        element.PizzaName = model.PizzaName;
                        element.Price = model.Price;
                        context.SaveChanges();
                        if (model.Id.HasValue)
                        {
                            var PizzaIngs = context.PizzaIngs.Where(rec
                           => rec.PizzaId == model.Id.Value).ToList();
                            // удалили те, которых нет в модели
                            context.PizzaIngs.RemoveRange(PizzaIngs.Where(rec =>
                            !model.PizzaIngs.ContainsKey(rec.IngredientId)).ToList());
                            context.SaveChanges();
                            // обновили количество у существующих записей
                            foreach (var updateIngredient in PizzaIngs)
                            {
                                updateIngredient.Count =
                               model.PizzaIngs[updateIngredient.IngredientId].Item2;

                                model.PizzaIngs.Remove(updateIngredient.IngredientId);
                            }
                            context.SaveChanges();
                        }
                        // добавили новые
                        foreach (var pc in model.PizzaIngs)
                        {
                            context.PizzaIngs.Add(new PizzaIng

                            {
                                PizzaId = element.Id,
                                IngredientId = pc.Key,
                                Count = pc.Value.Item2
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(PizzaBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        // удаяем записи по компонентам при удалении изделия
                        context.PizzaIngs.RemoveRange(context.PizzaIngs.Where(rec =>
                        rec.PizzaId == model.Id));
                        Pizza element = context.Pizzas.FirstOrDefault(rec => rec.Id
                       == model.Id);
                        if (element != null)
                        {
                            context.Pizzas.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Pizzas
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
               .Select(rec => new PizzaViewModel
               {
                   Id = rec.Id,
                   PizzaName = rec.PizzaName,
                   Price = rec.Price,
                   PizzaIngs = context.PizzaIngs
                .Include(recPC => recPC.Ingredient)
               .Where(recPC => recPC.PizzaId == rec.Id)
               .ToDictionary(recPC => recPC.IngredientId, recPC =>
                (recPC.Ingredient?.IngredientName, recPC.Count))
               })
               .ToList();
            }
        }
    }
}
