using System;
using System.Collections.Generic;

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
        
        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
