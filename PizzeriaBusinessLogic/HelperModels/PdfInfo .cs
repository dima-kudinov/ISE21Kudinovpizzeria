using System;
using PizzeriaBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportPizzaIngViewModel>PizzaIngs { get; set; }
    }
}
