using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaFileImplement.Implements
{
    public class StorageLogic : IStorageLogic
    {
        private readonly FileDataListSingleton source;
        public StorageLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public List<StorageViewModel> GetList()
        {
            return source.Storages.Select(rec => new StorageViewModel
            {
                Id = rec.Id,
                StorageName = rec.StorageName,
                StorageIngredients = source.StorageIngredients.Where(z => z.StorageId == rec.Id).Select(x => new StorageIngredientViewModel
                {
                    Id = x.Id,
                    StorageId = x.StorageId,
                    IngredientId = x.IngredientId,
                    IngredientName = source.Ingredients.FirstOrDefault(y => y.Id == x.IngredientId)?.IngredientName,
                    Count = x.Count
                }).ToList()
            })
                .ToList();
        }
        public StorageViewModel GetElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
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
                    StorageIngredients = source.StorageIngredients.Where(z => z.StorageId == elem.Id).Select(x => new StorageIngredientViewModel
                    {
                        Id = x.Id,
                        StorageId = x.StorageId,
                        IngredientId = x.IngredientId,
                        IngredientName = source.Ingredients.FirstOrDefault(y => y.Id == x.IngredientId)?.IngredientName,
                        Count = x.Count
                    }).ToList()
                };
            }
        }

        public void AddElement(StorageBindingModel model)
        {

            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }
        public void UpdElement(StorageBindingModel model)
        {
            var elem = source.Storages.FirstOrDefault(x => x.StorageName == model.StorageName && x.Id != model.Id);
            if (elem != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            var elemToUpdate = source.Storages.FirstOrDefault(x => x.Id == model.Id);
            if (elemToUpdate != null)
            {
                elemToUpdate.StorageName = model.StorageName;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public void DelElement(int id)
        {
            var elem = source.Storages.FirstOrDefault(x => x.Id == id);
            if (elem != null)
            {
                source.Storages.Remove(elem);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void FillStorage(StorageIngredientBindingModel model)
        {
            var item = source.StorageIngredients.FirstOrDefault(x => x.IngredientId == model.IngredientId
                    && x.StorageId == model.StorageId);

            if (item != null)
            {
                item.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageIngredients.Count > 0 ? source.StorageIngredients.Max(rec => rec.Id) : 0;
                source.StorageIngredients.Add(new StorageIngredient
                {
                    Id = maxId + 1,
                    StorageId = model.StorageId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
        }

        public bool CheckIngredientsAvailability(int PizzaId, int PizzasCount)
        {
            bool result = true;
            var PizzaIngs = source.PizzaIngs.Where(x => x.PizzaId == PizzaId);
            if (PizzaIngs.Count() == 0) return false;
            foreach (var elem in PizzaIngs)
            {
                int count = 0;
                var storageIngredients = source.StorageIngredients.FindAll(x => x.IngredientId == elem.IngredientId);
                count = storageIngredients.Sum(x=> x.Count);
                if (count < elem.Count * PizzasCount)
                    return false;
            }
            return result;
        }

        public void RemoveFromStorage(int PizzaId, int PizzasCount)
        {
            var PizzaIngs = source.PizzaIngs.Where(x => x.PizzaId == PizzaId);
            if (PizzaIngs.Count() == 0) return;
            foreach (var elem in PizzaIngs)
            {
                int left = elem.Count * PizzasCount;
                var storageIngredients = source.StorageIngredients.FindAll(x => x.IngredientId == elem.IngredientId);
                foreach (var rec in storageIngredients)
                {
                    int toRemove = left > rec.Count ? rec.Count : left;
                    rec.Count -= toRemove;
                    left -= toRemove;
                    if (left == 0) break;
                }
            }
            return;
        }
    }
}