using CashOverflow.Models.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace CashOverflow.Models
{
    public class Category
    {
        public Category()
        {
            this.Transactions = new List<Transaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [EnumDataType(typeof(CategoryType))]
        public CategoryType Type { get; set; }

        public string ImagePath { get; set; }

        public virtual IdentityUser User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
