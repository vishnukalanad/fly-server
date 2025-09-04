using fly_server.Constants;
using fly_server.Enums;
using fly_server.Models;
using fly_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace fly_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TravelController : ControllerBase
{
    private readonly ITravelService _travelService;
    public TravelController(ITravelService travelService)
    {
        _travelService = travelService;
    }


    [HttpGet("getAirlines")]
    public IActionResult GetAirlines(string? name)
    {
        IEnumerable<AirlineModel> airlines = _travelService.GetAirlines(name);
        if (!airlines.Any())
        {
            return NotFound(new ResponseModel()
            {
                StatusCode = 200,
                StatusMessage = ErrorMessages.ErrorMaps[ApiErrorKey.NoDataFound]
            });
        }
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Successfully retrieved {airlines.Count()} airlines.",
            Body = airlines
        });
    }

    [HttpPost("addAirline")]
    public IActionResult AddAirline(AirlineDto airline)
    {
        int result = _travelService.InsertUpdateAirline(airline);
        if (result == 0)
            return BadRequest(new ResponseModel()
            {
                StatusCode = 400,
                StatusMessage =
                    ErrorMessages.ErrorMaps[
                        airline.Id.HasValue ? ApiErrorKey.FailedToUpdateAirline : ApiErrorKey.FailedToInsertAirline],

            });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Successfully added airline.",
        });
    }
    
}