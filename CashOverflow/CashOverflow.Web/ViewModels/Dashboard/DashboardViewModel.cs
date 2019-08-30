using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        //public List<TransactionViewModel> Transactions { get; set; }

        public List<DashboardTransactionViewModel> Transactions { get; set; }
        
        public List<DashboardTodoViewModel> Todos { get; set; }

        public List<DashboardNoteViewModel> Notes { get; set; }
    }
}
