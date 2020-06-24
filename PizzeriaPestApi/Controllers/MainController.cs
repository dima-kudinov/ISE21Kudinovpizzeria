using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaBusinessLogic;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaRestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaBusinessLogic.BusinessLogics;

namespace PizzeriaRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IPizzaLogic _pizza;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, IPizzaLogic pizza, MainLogic main)
        {
            _order = order;
            _pizza = pizza;
            _main = main;
        }
        [HttpGet]
        public List<PizzaModel>  GetPizzaList() => _pizza.Read(null)?.Select(rec =>
       Convert(rec)).ToList();
        [HttpGet]
        public PizzaModel GetPizza(int PizzaId) => Convert(_pizza.Read(new
       PizzaBindingModel
        { Id = PizzaId })?[0]);
        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new
       OrderBindingModel
        { ClientId = clientId });
        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) =>
       _main.CreateOrder(model);
        private PizzaModel Convert(PizzaViewModel model)
        {
            if (model == null) return null;
            return new PizzaModel
            {
                Id = model.Id,
                PizzaName = model.PizzaName,
                Price = model.Price
            };
        }
    }
}