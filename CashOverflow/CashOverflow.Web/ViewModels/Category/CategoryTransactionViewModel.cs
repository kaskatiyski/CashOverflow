using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Category
{
    public class CategoryTransactionViewModel
    {
        public string Id { get; set; }

        public decimal Ammount { get; set; }

        public string Recipient { get; set; }

        public string Notes { get; set; }

        public DateTime Date { get; set; }

        public virtual TransactionLocationViewModel Location { get; set; }
    }
}
