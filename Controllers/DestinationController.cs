using fly_server.Constants;
using fly_server.DTOs;
using fly_server.Enums;
using fly_server.Models;
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
    [HttpGet("getDestinations")]
    public IActionResult GetDestinations(string? name, string? location)
    {
        IEnumerable<DestinationModel> results = _destinationService.GetAllDestinations(name, location);
        if(!results.Any()) return NotFound(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"{ErrorMessages.ErrorMaps[ApiErrorKey.NoDataFound]}",
            Body = results
        });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
            Body = results
        });
    }

    [HttpPost("createDestination")]
    public IActionResult AddDestination(DestinationDto request)
    {
        int result = _destinationService.InsertDestination(request);
        if (result == 0)
            return BadRequest(new ResponseModel()
            {
                StatusCode = 400,
                StatusMessage = $"Failed! {ErrorMessages.ErrorMaps[ApiErrorKey.DestinationAddFailed]}"
            });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
            Body = $"Successfully inserted {result.ToString()} record(s)"
        });
    }
}