using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Category
{
    public class CreateCategoryInputModel
    {
        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }
    }
}
