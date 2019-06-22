﻿using Microsoft.AspNetCore.Identity;
using System;

namespace CashOverflow.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Ammount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}