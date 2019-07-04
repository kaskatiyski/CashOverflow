using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class AllTransactionsViewModel
    {
        public IEnumerable<IGrouping<string, TransactionViewModel>> Transactions { get; set; }

        public IEnumerable<IGrouping<string, TransactionCategoryViewModel>> Categories { get; set; }
    }
}
