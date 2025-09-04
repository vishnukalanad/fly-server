using fly_server.Data;
using fly_server.Models;

namespace fly_server.Services;

public class TravelService : ITravelService
{
    private readonly DataContext _dapper;
    
    // SQL Statements;

    private readonly string _insertUpdateAirline = "FlyDbSchema.spAddAirlines";
    private readonly string _deleteAirline = "FlyDbSchema.spDeleteAirline";
    private readonly string _getAirlines = "FlyDbSchema.spGetAirlines";
    
    private readonly string _insertUpdateTrip = "";
    private readonly string _deleteTrip = "";
    
    private readonly string _insertUpdateHotel = "";
    private readonly string _deleteHotel = "";
    
    // SQL Statements end;
    
    
    public TravelService(IConfiguration configuration)
    {
        _dapper = new (configuration);
    }


    // Airline services;
    public int InsertUpdateAirline(AirlineDto airline)
    {
        return _dapper.ExecuteQuery(_insertUpdateAirline, new
        {
            Id = airline.Id ?? null,
            Name = airline.AirlineName,
            Code = airline.Code,
            Country = airline.Country,
            Rating = airline.Rating,
            Logo = airline.Logo,
            Website = airline.Website,
        }, true);
    }

    public int DeleteAirline(int id)
    {
        return _dapper.ExecuteQuery(_deleteAirline, new
        {
            Id = id
        }, true);
    }

    public IEnumerable<AirlineModel> GetAirlines(string? name)
    {
        return _dapper.LoadData<AirlineModel>(_getAirlines, new { Name = name });
    }
    
    // Trip services;
    public int InsertUpdateTrip(TripModel trip)
    {
        throw new NotImplementedException();
    }

    public int UpdateTrip(TripModel trip)
    {
        throw new NotImplementedException();
    }

    public int DeleteTrip(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TripModel> GetTrips()
    {
        throw new NotImplementedException();
    }
    
    // Hotel service;
    public int InsertUpdateHotel(HotelModel hotel)
    {
        throw new NotImplementedException();
    }

    public int UpdateHotel(HotelModel hotel)
    {
        throw new NotImplementedException();
    }

    public int DeleteHotel(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<HotelModel> GetHotels()
    {
        throw new NotImplementedException();
    }
}