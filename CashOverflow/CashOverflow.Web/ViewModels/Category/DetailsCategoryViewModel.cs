using CashOverflow.Models.Enum;
using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Category
{
    public class DetailsCategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [EnumDataType(typeof(CategoryType))]
        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<IGrouping<string, TransactionViewModel>> Transactions { get; set; }
    }
}
