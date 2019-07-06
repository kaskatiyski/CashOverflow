using CashOverflow.Models;
using CashOverflow.Services.Contracts;
using CashOverflow.Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CashOverflow.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext db;

        public LocationService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Location> CreateAsync(Location location)
        {
            var exists = await this.GetLocationByPlaceIdAsync(location.PlaceId);

            if (exists == null)
            {
                this.db.Add(location);
                await this.db.SaveChangesAsync();

                return location;
            }

            return exists;
        }

        public async Task<Location> GetLocationByPlaceIdAsync(string placeId)
        {
            Location location = await this.db.Locations.FirstOrDefaultAsync(l => l.PlaceId == placeId);

            return location;
        }
    }
}
