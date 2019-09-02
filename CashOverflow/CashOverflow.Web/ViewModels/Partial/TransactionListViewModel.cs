using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Partial
{
    public class TransactionListViewModel
    {
        public bool ShowDate { get; set; } = false;

        public bool IsCategoryView { get; set; } = false;

        public IEnumerable<IGrouping<string, TransactionViewModel>> Transactions { get; set; }
    }
}
