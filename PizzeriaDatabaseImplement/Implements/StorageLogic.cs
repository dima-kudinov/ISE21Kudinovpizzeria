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
    public class StorageLogic : IStorageLogic
    {
        public List<StorageViewModel> GetList()
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.Storages
                .ToList()
               .Select(rec => new StorageViewModel
               {
                   Id = rec.Id,
                   StorageName = rec.StorageName,
                   StorageIngredients = context.StorageIngredients
                .Include(recSF => recSF.Ingredient)
               .Where(recSF => recSF.StorageId == rec.Id).
               Select(x => new StorageIngredientViewModel
               {
                   Id = x.Id,
                   StorageId = x.StorageId,
                   IngredientId = x.IngredientId,
                   IngredientName = context.Ingredients.FirstOrDefault(y => y.Id == x.IngredientId).IngredientName,
                   Count = x.Count
               })
               .ToList()
               })
            .ToList();
            }
        }
        public StorageViewModel GetElement(int id)
        {
            using (var context = new PizzeriaDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem == null)
                {
                    throw new Exception("Элемент не найден");
                }
                else
                {
                    return new StorageViewModel
                    {
                        Id = id,
                        StorageName = elem.StorageName,
                        StorageIngredients = context.StorageIngredients
                .Include(recSF => recSF.Ingredient)
               .Where(recSF => recSF.StorageId == elem.Id)
                        .Select(x => new StorageIngredientViewModel
                        {
                            Id = x.Id,
                            StorageId = x.StorageId,
                            IngredientId = x.IngredientId,
                            IngredientName = context.Ingredients.FirstOrDefault(y => y.Id == x.IngredientId).IngredientName,
                            Count = x.Count
                        }).ToList()
                    };
                }
            }
        }
        public void AddElement(StorageBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var storage = new Storage();
                context.Storages.Add(storage);
                storage.StorageName = model.StorageName;
                context.SaveChanges();
            }
        }
        public void UpdElement(StorageBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var elemToUpdate = context.Storages.FirstOrDefault(x => x.Id == model.Id);
                if (elemToUpdate != null)
                {
                    elemToUpdate.StorageName = model.StorageName;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void DelElement(int id)
        {
            using (var context = new PizzeriaDatabase())
            {
                var elem = context.Storages.FirstOrDefault(x => x.Id == id);
                if (elem != null)
                {
                    context.Storages.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void FillStorage(StorageIngredientBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                var item = context.StorageIngredients.FirstOrDefault(x => x.IngredientId == model.IngredientId
    && x.StorageId == model.StorageId);

                if (item != null)
                {
                    item.Count += model.Count;
                }
                else
                {
                    var elem = new StorageIngredient();
                    context.StorageIngredients.Add(elem);
                    elem.StorageId = model.StorageId;
                    elem.IngredientId = model.IngredientId;
                    elem.Count = model.Count;
                }
                context.SaveChanges();
            }
        }      
        public void RemoveFromStorage(int pizzaId, int pizzasCount)
        {
            using (var context = new PizzeriaDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var pizzaIngs = context.PizzaIngs.Where(x => x.PizzaId == pizzaId);
                        if (pizzaIngs.Count() == 0) return;
                        foreach (var elem in pizzaIngs)
                        {
                            int left = elem.Count * pizzasCount;
                            var StorageIngredients = context.StorageIngredients.Where(x => x.IngredientId == elem.IngredientId);
                            int available = StorageIngredients.Sum(x => x.Count);
                            if (available < left) throw new Exception("Недостаточно продуктов на складе");
                            foreach (var rec in StorageIngredients)
                            {
                                int toRemove = left > rec.Count ? rec.Count : left;
                                rec.Count -= toRemove;
                                left -= toRemove;
                                if (left == 0) break;
                            }
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        return;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}