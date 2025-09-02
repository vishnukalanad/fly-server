using fly_server.Constants;
using fly_server.DTOs;
using fly_server.Enums;
using fly_server.Helpers;
using fly_server.Models;
using fly_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace fly_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly Auth _authHelper;

    public AuthController(IAuthService authService, IUserService userService, Auth authHelper)
    {
        _authService = authService;
        _authHelper = authHelper;
        _userService = userService;
    }

    [HttpPost("userLogin")]
    public IActionResult LoginUser(UserLoginDto request)
    {
        var data = _authService.RetrievePasswordAndHash(request);
        if (data.PasswordHash.Equals(null))
            return BadRequest(new ResponseModel()
            {
                StatusCode = 400,
                StatusMessage = $"Failed{ErrorMessages.ErrorMaps[ApiErrorKey.InvalidCredentials]}"
            });

        byte[] receivedPasswordHash = _authHelper.GeneratePasswordHash(request.Password, data.PasswordSalt);
        for (int i = 0; i < receivedPasswordHash.Length; i++)
        {
            Console.Write(data.PasswordHash[i]);
            if (data.PasswordHash[i] != receivedPasswordHash[i])
            {
                return BadRequest(new ResponseModel()
                {
                    StatusCode = 400,
                    StatusMessage = $"Failed {ErrorMessages.ErrorMaps[ApiErrorKey.InvalidCredentials]}"
                });
            }
        }

        IEnumerable<User> user = _userService.GetUsers(request.Email);
        string token = _authHelper.GenerateToken(user.First().Id);

        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
            Body =
                new
                {
                    token = token
                }
        });
    }
}