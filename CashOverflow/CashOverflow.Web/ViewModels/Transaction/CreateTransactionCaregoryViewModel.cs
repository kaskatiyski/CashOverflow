using CashOverflow.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class CreateTransactionCaregoryViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }
    }
}
