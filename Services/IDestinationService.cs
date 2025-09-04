using fly_server.DTOs;
using fly_server.Models;

namespace fly_server.Services;

public interface IDestinationService
{
    public int InsertUpdateDestination(DestinationDto request, string[] tags);
    public int DeleteDestination(int id);
    public IEnumerable<DestinationModel> GetAllDestinations(string? name, string? description);
}