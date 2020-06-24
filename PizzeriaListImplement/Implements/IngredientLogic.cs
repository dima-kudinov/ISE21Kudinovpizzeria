using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaListImplement.Models;

namespace PizzeriaListImplement.Implements
{
    public class IngredientLogic : IIngredientLogic
    {
        private readonly DataListSingleton source;
        public IngredientLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(IngredientBindingModel model)
        {
            Ingredient tempIngredient = model.Id.HasValue ? null : new Ingredient
            {
                Id = 1
            };
            foreach (var Ingredient in source.Ingredients)
            {
                if (Ingredient.IngredientName == model.IngredientName && Ingredient.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть такой ингредиент");
                }
                if (!model.Id.HasValue && Ingredient.Id >= tempIngredient.Id)
                {
                    tempIngredient.Id = Ingredient.Id + 1;
                }
                else if (model.Id.HasValue && Ingredient.Id == model.Id)
                {
                    tempIngredient = Ingredient;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempIngredient == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempIngredient);
            }
            else
            {
                source.Ingredients.Add(CreateModel(model, tempIngredient));
            }
        }
        public void Delete(IngredientBindingModel model)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == model.Id.Value)
                {
                    source.Ingredients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        public List<IngredientViewModel> Read(IngredientBindingModel model)
        {
            List<IngredientViewModel> result = new List<IngredientViewModel>();
            foreach (var Ingredient in source.Ingredients)
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
        private Ingredient CreateModel(IngredientBindingModel model, Ingredient Ingredient)
        {
            Ingredient.IngredientName = model.IngredientName;
            return Ingredient;
        }
        private IngredientViewModel CreateViewModel(Ingredient Ingredient)
        {
            return new IngredientViewModel
            {
                Id = Ingredient.Id,
                IngredientName = Ingredient.IngredientName
            };
        }
    }
}
