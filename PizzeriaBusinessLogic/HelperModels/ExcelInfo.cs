using System;
using PizzeriaBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PizzeriaBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<IGrouping<DateTime, OrderViewModel>> Orders { get; set; }
    }
}
