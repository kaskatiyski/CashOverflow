using CashOverflow.Models;

namespace CashOverflow.Services.Contracts
{
    public interface ILocationService
    {
        Location Create(Location location);

        Location GetLocationByPlaceId(string placeId);
    }
}
