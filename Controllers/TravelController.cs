using System.Data;
using System.Text.Json;
using fly_server.Constants;
using fly_server.Enums;
using fly_server.Helpers;
using fly_server.Models;
using fly_server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace fly_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TravelController : ControllerBase
{
    private readonly ITravelService _travelService;
    private readonly DataTableGenerator _dataTableGenerator;
    public TravelController(ITravelService travelService, DataTableGenerator datTableGenerator)
    {
        _travelService = travelService;
        _dataTableGenerator = datTableGenerator;
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

    [HttpPost("addUpdateAirline")]
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

    [HttpDelete("deleteAirline")]
    public IActionResult DeleteAirline(int id)
    {
        int result = _travelService.DeleteAirline(id);
        if (result == 0)
            return BadRequest(new ResponseModel()
            {
                StatusCode = 400,
                StatusMessage =
                    ErrorMessages.ErrorMaps[ApiErrorKey.FailedToDeleteAirline],

            });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Successfully deleted airline.",
        });
    }

    [HttpPost("addUpdateHotels")]
    public IActionResult AddHotels([FromBody] HotelDto hotel)
    {
        var amenities = hotel.Amenities;
        DataTable amenitiesTvp = _dataTableGenerator.CreateNestedDataTable(amenities);
        
        int result = _travelService.InsertUpdateHotel(hotel, amenitiesTvp);
        if (result == 0) return BadRequest();
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Successfully added hotel.",
        });
    }

    [HttpDelete("deleteHotel")]
    public IActionResult DeleteHotel(int id)
    {
        int result = _travelService.DeleteHotel(id);
        if (result == 0)
            return BadRequest(new ResponseModel()
            {
                StatusCode = 400,
                StatusMessage =
                    ErrorMessages.ErrorMaps[ApiErrorKey.FailedToDeleteHotel],

            });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Successfully deleted hotel.",
        });
    }
    
    [HttpGet("getHotels")]
    public IActionResult GetHotels(string? name)
    {
        IEnumerable<HotelModel> hotels = _travelService.GetHotels(name);
        if (hotels.IsNullOrEmpty())
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
            StatusMessage = $"Successfully retrieved {hotels.Count()} airlines.",
            Body = hotels
        });
    }
    
}