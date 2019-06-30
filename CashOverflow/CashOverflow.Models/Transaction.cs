using Microsoft.AspNetCore.Identity;
using System;

namespace CashOverflow.Models
{
    public class Transaction
    {
        public Transaction()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public decimal Ammount { get; set; }

        public string Recipient { get; set; }

        public string Notes { get; set; }
               
        public DateTime Date { get; set; }

        public string LocationId { get; set; }

        public virtual Location Location { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
