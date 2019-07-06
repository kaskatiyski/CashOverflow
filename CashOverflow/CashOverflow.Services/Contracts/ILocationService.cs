using CashOverflow.Models;
using System.Threading.Tasks;

namespace CashOverflow.Services.Contracts
{
    public interface ILocationService
    {
        Task<Location> CreateAsync(Location location);

        Task<Location> GetLocationByPlaceIdAsync(string placeId);
    }
}
