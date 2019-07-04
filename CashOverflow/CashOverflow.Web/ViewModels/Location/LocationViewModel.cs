using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Web.ViewModels.Location
{
    public class LocationViewModel
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string PlaceId { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
