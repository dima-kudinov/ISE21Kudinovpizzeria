using System;
using PizzeriaBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.HelperModels
{
    class WordParagraphProperties
    {
        public string Size { get; set; }

        public bool Bold { get; set; }

        public JustificationValues JustificationValues { get; set; }
    }
}
