using CashOverflow.Web.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Calendar
{
    public class CalendarTransactionViewModel
    {
        public decimal Ammount { get; set; }

        public string Recipient { get; set; }

        public string Notes { get; set; }

        public DateTime Date { get; set; }

        public virtual CalendarCategoryViewModel Category { get; set; }

        public virtual CalendarLocationViewModel Location { get; set; }
    }
}
