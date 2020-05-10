﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.Serialization;

namespace PizzeriaBusinessLogic.ViewModels
{
    [DataContract]
    public class PizzaViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название пиццы")]
        public string PizzaName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> PizzaIngs { get; set; }
    }
}
