using CashOverflow.Web.ViewModels.Category;
using CashOverflow.Web.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class EditTransactionViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
