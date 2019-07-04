using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Calendar
{
    public class CalendarCategoryViewModel
    {
        public string Name { get; set; }

        [EnumDataType(typeof(CategoryType))]
        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }
    }
}
