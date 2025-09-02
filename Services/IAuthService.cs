using fly_server.DTOs;
using fly_server.Models;

namespace fly_server.Services;

public interface IAuthService
{
    public string UserLogin(UserLoginDto request);
    public LoginInstanceModel RetrievePasswordAndHash(UserLoginDto request);
}