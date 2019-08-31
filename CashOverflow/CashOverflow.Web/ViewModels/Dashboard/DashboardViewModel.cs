using CashOverflow.Web.ViewModels.Note;
using CashOverflow.Web.ViewModels.Todo;
using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public List<TransactionViewModel> Transactions { get; set; }
        
        public List<TodoViewModel> Todos { get; set; }

        public List<NoteViewModel> Notes { get; set; }
    }
}
