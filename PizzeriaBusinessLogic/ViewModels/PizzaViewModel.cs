using System;
using PizzeriaBusinessLogic.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.Serialization;

namespace PizzeriaBusinessLogic.ViewModels
{
    [DataContract]
    public class PizzaViewModel : BaseViewModel
    {
        [Column(title: "Название пиццы", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string PizzaName { get; set; }
        [Column(title: "Цена", width: 50)]
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> PizzaIngs { get; set; }
        public override List<string> Properties() => new List<string>
        {
            "Id",
            "PizzaName",
            "Price"
        };
    }
}
