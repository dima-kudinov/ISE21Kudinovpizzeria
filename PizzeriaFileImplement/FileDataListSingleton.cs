using PizzeriaBusinessLogic.Enums;
using PizzeriaFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PizzeriaFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string IngredientFileName = "Ingredient.xml";

        private readonly string OrderFileName = "Order.xml";

        private readonly string PizzaFileName = "Pizza.xml";

        private readonly string PizzaIngFileName = "PizzaIng.xml";
        private readonly string ClientFileName = "Client.xml";

        public List<Ingredient> Ingredients { get; set; }

        public List<Order> Orders { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public List<PizzaIng> PizzaIngs { get; set; }
        public List<Client> Clients { get; set; }

        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Orders = LoadOrders();
            Pizzas = LoadPizzas();
            PizzaIngs = LoadPizzaIngs();
            Clients = LoadClients();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null) { instance = new FileDataListSingleton(); }

            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveIngredients(); SaveOrders(); SaveClients(); SavePizzas(); SavePizzaIngs();
        }
        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }
        private List<Ingredient> LoadIngredients()
        {
            var list = new List<Ingredient>();

            if (File.Exists(IngredientFileName))
            {
                XDocument xDocument = XDocument.Load(IngredientFileName);

                var xElements = xDocument.Root.Elements("Ingredient").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Ingredient { Id = Convert.ToInt32(elem.Attribute("Id").Value), IngredientName = elem.Element("IngredientName").Value });
                }
            }

            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();

            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);

                var xElements = xDocument.Root.Elements("Order").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                        elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }

            return list;
        }

        private List<Pizza> LoadPizzas()
        {
            var list = new List<Pizza>();

            if (File.Exists(PizzaFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaFileName);
                var xElements = xDocument.Root.Elements("Pizza").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Pizza
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaName = elem.Element("PizzaName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }

            return list;
        }

        private List<PizzaIng> LoadPizzaIngs()
        {
            var list = new List<PizzaIng>();

            if (File.Exists(PizzaIngFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaIngFileName);

                var xElements = xDocument.Root.Elements("PizzaIng").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new PizzaIng
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                        IngredientId = Convert.ToInt32(elem.Element("IngredientId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }

            return list;
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");

                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
        private void SaveIngredients()
        {
            if (Ingredients != null)
            {
                var xElement = new XElement("Ingredients");

                foreach (var Ingredient in Ingredients)
                {
                    xElement.Add(new XElement("Ingredient",
                        new XAttribute("Id", Ingredient.Id),
                        new XElement("IngredientName", Ingredient.IngredientName)));
                }

                XDocument xDocument = new XDocument(xElement); xDocument.Save(IngredientFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");

                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order", new XAttribute("Id", order.Id),
                        new XElement("PizzaId", order.PizzaId),
                        new XElement("ClientId", order.ClientId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", order.Status),
                        new XElement("DateCreate", order.DateCreate),
                        new XElement("DateImplement", order.DateImplement)));
                }

                XDocument xDocument = new XDocument(xElement); xDocument.Save(OrderFileName);
            }
        }

        private void SavePizzas()
        {
            if (Pizzas != null)
            {
                var xElement = new XElement("Pizzas");

                foreach (var Pizza in Pizzas)
                {
                    xElement.Add(new XElement("Pizza", new XAttribute("Id", Pizza.Id),
                        new XElement("PizzaName", Pizza.PizzaName),
                        new XElement("Price", Pizza.Price)));
                }

                XDocument xDocument = new XDocument(xElement); xDocument.Save(PizzaFileName);
            }
        }

        private void SavePizzaIngs()
        {
            if (PizzaIngs != null)
            {
                var xElement = new XElement("PizzaIngs");


                foreach (var PizzaIng in PizzaIngs)
                {
                    xElement.Add(new XElement("PizzaIng", new XAttribute("Id", PizzaIng.Id),
                        new XElement("PizzaId", PizzaIng.PizzaId),
                        new XElement("IngredientId", PizzaIng.IngredientId),
                        new XElement("Count", PizzaIng.Count)));
                }

                XDocument xDocument = new XDocument(xElement); xDocument.Save(PizzaIngFileName);
            }
        }
    }
}
