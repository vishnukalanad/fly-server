using fly_server.DTOs;
using fly_server.Models;

namespace fly_server.Services;

public interface IDestinationService
{
    public int InsertDestination(DestinationDto request);
    public int UpdateDestination(DestinationDto request);
    public int DeleteDestination(int id);
    public IEnumerable<DestinationModel> GetAllDestinations(string? name, string? description);
}