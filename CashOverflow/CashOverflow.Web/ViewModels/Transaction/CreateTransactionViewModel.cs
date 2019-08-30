using CashOverflow.Web.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class CreateTransactionViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
