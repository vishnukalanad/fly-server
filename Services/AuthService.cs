using fly_server.Data;
using fly_server.DTOs;
using fly_server.Helpers;
using fly_server.Models;

namespace fly_server.Services;

public class AuthService: IAuthService
{
    private readonly DataContext _context;
    private readonly Auth _auth;
    
    // SQL Queries;

    private readonly string _retrieveLoginInstance = $"select Email, PasswordHash, PasswordSalt from FlyDbSchema.Login where Email = @Email";
    
    // End of SQL Queries;
    
    public AuthService(IConfiguration config, Auth authHelper)
    {
        _context = new DataContext(config);
        _auth = authHelper;
    }

    public string UserLogin(UserLoginDto request)
    {
        return "";
    }

    public LoginInstanceModel RetrievePasswordAndHash(UserLoginDto request)
    {
        var parameters = new
        {
            Email = request.Email,
        };
        return _context.LoadSingleDatum<LoginInstanceModel>(_retrieveLoginInstance, parameters);
    }
}