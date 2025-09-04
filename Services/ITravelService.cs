using fly_server.Models;

namespace fly_server.Services;

public interface ITravelService
{
    // Airline endpoints;
    public int InsertAirline(AirlineModel airline);
    public int UpdateAirline(AirlineModel airline);
    public int DeleteAirline(string id);
    public IEnumerable<AirlineModel> GetAirlines();
    
    // Trip endpoints;
    public int InsertTrip(TripModel trip);
    public int UpdateTrip(TripModel trip);
    public int DeleteTrip(string id);
    public IEnumerable<TripModel> GetTrips();
    
    // Hotel endpoints;
    public int InsertHotel(HotelModel hotel);
    public int UpdateHotel(HotelModel hotel);
    public int DeleteHotel(string id);
    public IEnumerable<HotelModel> GetHotels();
}