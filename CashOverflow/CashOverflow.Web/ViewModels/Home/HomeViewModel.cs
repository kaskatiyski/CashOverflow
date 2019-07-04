using CashOverflow.Web.ViewModels.Transaction;
using System.Collections.Generic;

namespace CashOverflow.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<TransactionViewModel> Transactions { get; set; }
    }
}