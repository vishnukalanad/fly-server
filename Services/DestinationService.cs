using System.Data;
using fly_server.Data;
using fly_server.DTOs;
using fly_server.Helpers;
using fly_server.Models;

namespace fly_server.Services;

public class DestinationService : IDestinationService
{
    private readonly DataContext _dataContext;
    private readonly DataTableGenerator _dataTableGenerator;

    // SQL Queries begin;

    // private readonly string _insertDestination =
    //     $"insert into FlyDbSchema.Destinations (Name, Description, Location, Price, Image, AnnualVisitors) values (@Name, @Description, @Location, @Price, @Image, @AnnualVisitors)";

    private readonly string _insertDestination =
        $"FlyDbSchema.spCreateDestination";
    private readonly string _getDestinations = $"exec FlyDbSchema.spGetDestinations @Location, @Name";

    private readonly string _deleteDestination = $"exec FlyDbSchema.spDelete_Destination @Id";

    // SQL Queries end
    public DestinationService(IConfiguration config, DataTableGenerator dataTableGenerator)
    {
        _dataContext = new(config);
        _dataTableGenerator = dataTableGenerator;
    }

    public IEnumerable<DestinationModel> GetAllDestinations(string? name, string? location)
    {
        return _dataContext.LoadData<DestinationModel>(_getDestinations, new
        {
            Name = name ?? null,
            Location = location ?? null,
        });
    }

    public int InsertDestination(DestinationDto request, string[] tags)
    {
        DataTable tagsDataTable = _dataTableGenerator.CreateDataTableValues("Tag", tags);
        return _dataContext.ExecuteQuery(_insertDestination, new
        {
            Name = request.Name,
            Description = request.Description,
            Location = request.Location,
            Price = request.Price,
            Image = request.Image,
            AnnualVisitors = request.AnnualVisitors,
            Tags = tagsDataTable,
        }, true, "FlyDbSchema.TagTableType");
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