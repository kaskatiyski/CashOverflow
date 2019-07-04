using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using System.Linq;

namespace CashOverflow.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext db;

        public LocationService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Location Create(Location location)
        {
            var exists = this.GetLocationByPlaceId(location.PlaceId);

            if (exists == null)
            {
                this.db.Add(location);
                this.db.SaveChanges();

                return location;
            }

            return exists;
        }

        public Location GetLocationByPlaceId(string placeId)
        {
            Location location = this.db.Locations.FirstOrDefault(l => l.PlaceId == placeId);

            return location;
        }
    }
}
