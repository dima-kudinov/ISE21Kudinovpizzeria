﻿using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaListImplement.Implements
{
    public class PizzaLogic : IPizzaLogic
    {
        private readonly DataListSingleton source;
        public PizzaLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(PizzaBindingModel model)
        {
            Pizza tempPizza = model.Id.HasValue ? null : new Pizza { Id = 1 };
            foreach (var Pizza in source.Pizzas)
            {
                if (Pizza.PizzaName == model.PizzaName && Pizza.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && Pizza.Id >= tempPizza.Id)
                {
                    tempPizza.Id = Pizza.Id + 1;
                }
                else if (model.Id.HasValue && Pizza.Id == model.Id)
                {
                    tempPizza = Pizza;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempPizza == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempPizza);
            }
            else
            {
                source.Pizzas.Add(CreateModel(model, tempPizza));
            }
        }
        public void Delete(PizzaBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.PizzaIng.Count; ++i)
            {
                if (source.PizzaIng[i].PizzaId == model.Id)
                {
                    source.PizzaIng.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Pizzas.Count; ++i)
            {
                if (source.Pizzas[i].Id == model.Id)
                {
                    source.Pizzas.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Pizza CreateModel(PizzaBindingModel model, Pizza Pizza)
        {
            Pizza.PizzaName = model.PizzaName;
            Pizza.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.PizzaIng.Count; ++i)
            {
                if (source.PizzaIng[i].Id > maxPCId)
                {
                    maxPCId = source.PizzaIng[i].Id;
                }
                if (source.PizzaIng[i].PizzaId == Pizza.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.PizzaIng.ContainsKey(source.PizzaIng[i].IngredientId))
                    {
                        // обновляем количество
                        source.PizzaIng[i].Count =
                        model.PizzaIng[source.PizzaIng[i].IngredientId].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные


                        model.PizzaIng.Remove(source.PizzaIng[i].IngredientId);
                    }
                    else
                    {
                        source.PizzaIng.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var pc in model.PizzaIng)
            {
                source.PizzaIng.Add(new PizzaIng
                {
                    Id = ++maxPCId,
                    PizzaId = Pizza.Id,
                    IngredientId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return Pizza;
        }
        public List<PizzaViewModel> Read(PizzaBindingModel model)
        {
            List<PizzaViewModel> result = new List<PizzaViewModel>();
            foreach (var Ingredient in source.Pizzas)
            {
                if (model != null)
                {
                    if (Ingredient.Id == model.Id)
                    {
                        result.Add(CreateViewModel(Ingredient));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(Ingredient));
            }
            return result;
        }
        private PizzaViewModel CreateViewModel(Pizza Pizza)
        {
            Dictionary<int, (string, int)> PizzaIng = new Dictionary<int,
    (string, int)>();
            foreach (var pc in source.PizzaIng)
            {
                if (pc.PizzaId == Pizza.Id)
                {
                    string IngredientName = string.Empty;
                    foreach (var Ingredient in source.Ingredients)
                    {
                        if (pc.IngredientId == Ingredient.Id)
                        {
                            IngredientName = Ingredient.IngredientName;
                            break;
                        }
                    }
                    PizzaIng.Add(pc.IngredientId, (IngredientName, pc.Count));
                }
            }
            return new PizzaViewModel
            {
                Id = Pizza.Id,
                PizzaName = Pizza.PizzaName,
                Price = Pizza.Price,
                PizzaIng = PizzaIng
            };
        }
    }
}
