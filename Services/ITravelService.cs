using fly_server.Models;

namespace fly_server.Services;

public interface ITravelService
{
    // Airline endpoints;
    public int InsertUpdateAirline(AirlineDto airline);
    public int DeleteAirline(int id);
    public IEnumerable<AirlineModel> GetAirlines(string? name);
    
    // Trip endpoints;
    public int InsertUpdateTrip(TripModel trip);
    public int DeleteTrip(string id);
    public IEnumerable<TripModel> GetTrips();
    
    // Hotel endpoints;
    public int InsertUpdateHotel(HotelModel hotel);
    public int DeleteHotel(string id);
    public IEnumerable<HotelModel> GetHotels();
}