using fly_server.DTOs;
using fly_server.Models;
using fly_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace fly_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("userLogin")]
    public IActionResult LoginUser(UserLoginDto request)
    {
        var data = _authService.RetrievePasswordAndHash(request);
        return Ok(new
        {
            data = data,
        });
    }
}