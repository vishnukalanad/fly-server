using fly_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace fly_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DestinationController : ControllerBase
{
    private readonly IDestinationService _destinationService;
    public DestinationController(IDestinationService destinationService)
    {
        _destinationService = destinationService;
    }
}