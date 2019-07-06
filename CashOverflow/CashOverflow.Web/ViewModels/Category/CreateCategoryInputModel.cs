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
        [Required]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(CategoryType))]
        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }
    }
}
