using CashOverflow.Models.Enum;
using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }

        public IEnumerable<TransactionAmountViewModel> Transactions { get; set; }
    }
}
