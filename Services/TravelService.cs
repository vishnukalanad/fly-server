using System.Data;
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
    
    private readonly string _insertUpdateHotel = "FlyDbSchema.spAddUpdateHotels";
    private readonly string _deleteHotel = "FlyDbSchema.spDeleteHotel";
    private readonly string _getHotels = "FlyDbSchema.spGetHotel";
    
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
    
    // Hotel service;
    public int InsertUpdateHotel(HotelDto hotel, DataTable amenities)
    {
        return _dapper.ExecuteQuery(_insertUpdateHotel, new
        {
            Id = hotel.Id ?? null,
            Name = hotel.Name,
            Description = hotel.Description,
            Latitude = hotel.Latitude,
            Longitude = hotel.Longitude,
            City = hotel.City,
            Country = hotel.Country,
            Rating = hotel.Rating,
            Image = hotel.Image,
            Price = hotel.Price,
            Amenities = amenities,
        },  true, "FlyDbSchema.AmenityTableType");
    }

    public int DeleteHotel(int id)
    {
        return _dapper.ExecuteQuery(_deleteHotel, new
        {
            Id = id
        }, true);
    }

    public IEnumerable<HotelModel> GetHotels(string? name)
    {
        return _dapper.LoadJson<HotelModel>(_getHotels, new { Name = name });
    }
    
    // Trip services;
    public int InsertUpdateTrip(TripModel trip)
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
}