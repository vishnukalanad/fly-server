using fly_server.DTOs;
using fly_server.Helpers;
using fly_server.Models;

namespace fly_server.Services;

public interface IUserService
{
    public IEnumerable<User> GetUsers(string? email);
    public int CreateUser(UserAddDto user, byte[] passwordHash, byte[] passwordSalt);
    public bool UpdateUser(UserAddDto user);
    public bool DeleteUser(int id);
    public bool ChangeUserStatus(int id, bool status);
    public string GetCaptcha(out string captchaId);
    public bool ValidateCaptcha(CaptchaValidateDto request);
    public string ReCaptcha(string captchaId);
}