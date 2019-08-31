using CashOverflow.Models.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CashOverflow.Models
{
    public class RecurringPayment
    {
        public RecurringPayment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public int Amount { get; set; }

        public DateTime StartDate { get; set; }

        public int Payments { get; set; }

        public ushort Interval { get; set; }

        [EnumDataType(typeof(RecurringPeriod))]
        public RecurringPeriod Period { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
