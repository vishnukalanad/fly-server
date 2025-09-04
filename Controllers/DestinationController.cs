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
        IEnumerable<DestinationModelOut> outputModel = results.Select(r => new DestinationModelOut()
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Location = r.Location,
            Price = r.Price,
            Image = r.Image,
            AnnualVisits = r.AnnualVisits,
            Tags = r.Tags.Trim().Split(",").ToList()
        });
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
            Body = outputModel
        });
    }
    

    [HttpPost("createDestination")]
    public IActionResult AddDestination(DestinationDto request)
    {
        string[] tags = request.Tags.Trim().Split(',');
        int result = _destinationService.InsertUpdateDestination(request, tags);
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
            Body = $"Successfully inserted/updated {result.ToString()} record(s)"
        });
    }

    [HttpDelete("deleteDestination")]
    public IActionResult DeleteDestination(int id)
    {
        int result = _destinationService.DeleteDestination(id);
        if (result == 0) return StatusCode(500, new ResponseModel()
        {
            StatusCode = 500,
            StatusMessage = $"Failed! {ErrorMessages.ErrorMaps[ApiErrorKey.DestinationAddFailed]}"
        });

        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
            Body = $"Successfully deleted {result.ToString()} record(s)"
        });
    }
}