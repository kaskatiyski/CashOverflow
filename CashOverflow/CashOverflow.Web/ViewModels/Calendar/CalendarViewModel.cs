using CashOverflow.Web.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Calendar
{
    public class CalendarViewModel
    {
        public Dictionary<string, List<CalendarTransactionViewModel>> Transactions { get; set; }

        public Dictionary<string, List<CalendarTodoViewModel>> Todos { get; set; }
    }
}
