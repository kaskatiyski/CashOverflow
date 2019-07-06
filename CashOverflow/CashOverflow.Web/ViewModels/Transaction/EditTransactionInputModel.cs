﻿using CashOverflow.Web.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Transaction
{
    public class EditTransactionInputModel
    {
        public string Id { get; set; }

        public decimal Ammount { get; set; }

        public string Recipient { get; set; }

        public string Notes { get; set; }

        public DateTime Date { get; set; }

        public string CategoryId { get; set; }

        public string UserId { get; set; }

        public string LocationId { get; set; }

        public virtual CreateLocationInputModel Location { get; set; }        
    }
}