using CashOverflow.Web.ViewModels.Category;
using System;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class HomeTransactionViewModel
    {
        public int Id { get; set; }

        public decimal Ammount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public virtual HomeCategoryViewModel Category { get; set; }
    }
}
