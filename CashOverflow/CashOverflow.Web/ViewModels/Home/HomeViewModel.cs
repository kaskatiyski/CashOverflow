using CashOverflow.Web.ViewModels.Transaction;
using System.Collections.Generic;

namespace CashOverflow.Web.ViewModels.Home
{
    public class HomeViewModel
    {
        public IEnumerable<HomeTransactionViewModel> Transactions { get; set; }
    }
}