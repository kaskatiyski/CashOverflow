using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CashOverflow.Models
{
    public class CashOverflowUser : IdentityUser
    {
        public CashOverflowUser()
        {
            this.Transactions = new List<Transaction>();
            this.Categories = new List<Category>();
        }
        
        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

    }
}
