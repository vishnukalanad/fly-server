using fly_server.Data;
using fly_server.DTOs;
using fly_server.Models;

namespace fly_server.Services;

public class DestinationService : IDestinationService
{
    private readonly DataContext _dataContext;

    // SQL Queries begin;

    private readonly string _insertDestination =
        $"insert into FlyDbSchema.Destinations (Name, Description, Location, Price, Image, AnnualVisitors) values (@Name, @Description, @Location, @Price, @Image, @AnnualVisitors)";

    private readonly string _getDestinations = $"exec FlyDbSchema.spGetTags_Proc @Location, @Name";

    private readonly string _deleteDestination = $"exec FlyDbSchema.spDelete_Destination @Id";

    // SQL Queries end
    public DestinationService(IConfiguration config)
    {
        _dataContext = new(config);
    }

    public IEnumerable<DestinationModel> GetAllDestinations(string? name, string? location)
    {
        return _dataContext.LoadData<DestinationModel>(_getDestinations, new
        {
            Name = name ?? null,
            Location = location ?? null,
        });
    }

    public int InsertDestination(DestinationDto request)
    {
        return _dataContext.ExecuteQuery(_insertDestination, request);
    }

    public int UpdateDestination(DestinationDto request)
    {
        return 0;
    }

    public int DeleteDestination(int id)
    {
        int results = _dataContext.ExecuteQuery(_deleteDestination, new { Id = id });
        return results;
    }
}