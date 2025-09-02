using fly_server.Constants;
using fly_server.DTOs;
using fly_server.Enums;
using fly_server.Helpers;
using fly_server.Models;
using fly_server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace fly_server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("getAllUsers")]
    public IActionResult GetUsers()
    {
        IEnumerable<User> users = _userService.GetUsers();
        if (users.IsNullOrEmpty())
            return StatusCode(400,
                new ResponseModel()
                {
                    StatusCode = 400, StatusMessage = $"Failed! {ErrorMessages.ErrorMaps[ApiErrorKey.UserNotFound]}"
                });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = "Success",
            Body = users
        });
    }

    [HttpGet("getUserById")]
    public IActionResult GetUserById(int id)
    {
        User user = _userService.GetUserById(id);
        if (user.Equals(null))
            return StatusCode(400,
                new ResponseModel()
                {
                    StatusCode = 400, StatusMessage = $"Failed! {ErrorMessages.ErrorMaps[ApiErrorKey.UserNotFound]}"
                });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
            Body = user
        });
    }


    [HttpPost("addUser")]
    public IActionResult AddUser([FromBody] User user)
    {
        bool result = _userService.CreateUser(new());
        if (!result)
            return StatusCode(400, new ResponseModel()
            {
                StatusCode = 400,
                StatusMessage = $"Failed! {ErrorMessages.ErrorMaps[ApiErrorKey.CreateUserFailed]}",
            });
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success, User created!",
        });
    }

    [HttpGet("getCaptcha")]
    public IActionResult GetCaptcha()
    {
        string captcha = _userService.GetCaptcha(out string captchaId);
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
            Body =
            new {
                captcha = captcha,
                captchaId = captchaId
            }
        });
    }
    
    [HttpPost("validateCaptcha")]
    public IActionResult ValidateCaptcha(CaptchaValidateDto request)
    {
        if (!_userService.ValidateCaptcha(request)) return BadRequest(new ResponseModel()
        {
            StatusCode = 400,
            StatusMessage = $"Failed! {ErrorMessages.ErrorMaps[ApiErrorKey.InvalidCaptcha]}"
        });
        
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
        });
    }
    
    [HttpGet("reCaptcha")]
    public IActionResult ReCaptcha(string captchaId)
    {
        string captcha = _userService.ReCaptcha(captchaId);
        return Ok(new ResponseModel()
        {
            StatusCode = 200,
            StatusMessage = $"Success",
            Body =
                new {
                    captcha = captcha,
                    captchaId = captchaId
                }
        });
    }
}