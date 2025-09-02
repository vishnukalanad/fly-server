using fly_server.Data;
using fly_server.DTOs;
using fly_server.Models;

namespace fly_server.Services;

public class DestinationService: IDestinationService
{
    private readonly DataContext _dataContext;
    public DestinationService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public IEnumerable<DestinationModel> GetAllTrips(string? name, string? description)
    {
        return [];
    }

    public int InsertDestination(DestinationDto request)
    {
        return 0;
    }

    public int UpdateDestination(DestinationDto request)
    {
        return 0;
    }

    public int DeleteDestination(int id)
    {
        return 0;
    }
}