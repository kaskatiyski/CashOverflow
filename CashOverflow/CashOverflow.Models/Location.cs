using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashOverflow.Models
{
    public class Location
    {
        public Location()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Transactions = new List<Transaction>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PlaceId { get; set; }

        [Column(TypeName = "decimal(18,16)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(18,16)")]
        public decimal Longitude { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
