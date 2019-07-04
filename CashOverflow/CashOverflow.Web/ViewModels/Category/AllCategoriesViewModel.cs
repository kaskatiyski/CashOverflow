using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CashOverflow.Web.ViewModels.Category
{
    public class AllCategoriesViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
