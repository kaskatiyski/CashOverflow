using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class CreateTransactionViewModel
    {
        public IEnumerable<CreateTransactionCaregoryViewModel> Categories { get; set; }
    }
}
