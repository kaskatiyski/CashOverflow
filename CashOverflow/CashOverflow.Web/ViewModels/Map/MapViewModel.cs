using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Map
{
    public class MapViewModel
    {
        public IEnumerable<MapTransactionViewModel> Transactions { get; set; }

    }
}
