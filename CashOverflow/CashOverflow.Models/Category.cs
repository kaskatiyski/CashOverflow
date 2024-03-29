﻿using CashOverflow.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using System;

namespace CashOverflow.Models
{
    public class Category
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transactions = new List<Transaction>();
            this.RecurringPayments = new List<RecurringPayment>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        [EnumDataType(typeof(CategoryType))]
        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<RecurringPayment> RecurringPayments { get; set; }
    }
}
