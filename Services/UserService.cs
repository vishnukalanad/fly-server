using fly_server.Data;
using fly_server.DTOs;
using fly_server.Helpers;
using fly_server.Models;

namespace fly_server.Services;

public class UserService : IUserService
{
    private readonly DataContext _dapper;

    private readonly string _insertCaptcha =
        $"insert into FlyDbSchema.Captcha(captchaId, captchaString) values (@CaptchaId, @CaptchaString)";

    private readonly string _retrieveCaptcha =
        $"select captchaString from FlyDbSchema.Captcha where captchaId = @CaptchaId";

    private readonly string _removeCaptcha = $"delete from FlyDbSchema.Captcha where captchaId = @CaptchaId";
    private readonly string _recaptchaQuery = $"update FlyDbSchema.Captcha set captchaString = @Captcha  where captchaId = @CaptchaId";

    public UserService(IConfiguration config)
    {
        _dapper = new(config);
    }

    public IEnumerable<User> GetUsers()
    {
        return [];
    }

    public User GetUserById(int id)
    {
        return new();
    }

    public bool CreateUser(UserAddDto user)
    {
        return false;
    }

    public bool UpdateUser(UserAddDto user)
    {
        return false;
    }

    public bool DeleteUser(int id)
    {
        return false;
    }

    public bool ChangeUserStatus(int id, bool status)
    {
        return false;
    }
    // Generates captcha and captchaId;
    public string GetCaptcha(out string captchaId)
    {
        var cpGen = new CaptchaGenerator();
        Guid uuid = Guid.NewGuid();
        string captchaB64 = cpGen.GenerateCaptcha(out string captcha);
        if (_dapper.ExecuteQuery(_insertCaptcha, new
            {
                captchaId = uuid.ToString(),
                captchaString = captcha
            }) == 0)
        {
            captchaId = "";
            return "Failed to generate captcha! Please try again";
        }
        captchaId = uuid.ToString();
        return captchaB64;
    }

    // Validates captcha;
    public bool ValidateCaptcha(CaptchaValidateDto request)
    {
        string captchaFromTable = _dapper.LoadSingleDatum<string>(_retrieveCaptcha, new { CaptchaId = request.CaptchaId });
        _dapper.ExecuteQuery(_removeCaptcha, new { CaptchaId = request.CaptchaId });
        if (request.Captcha != captchaFromTable) return false;
        return true;
    }
    // reCaptcha -> to update the captcha string without creating a new table entry;
    public string ReCaptcha(string captchaId)
    {
        var captchaGenerator = new CaptchaGenerator();
        string captcha64 = captchaGenerator.GenerateCaptcha(out string captcha);
        
        if(_dapper.ExecuteQuery(_recaptchaQuery, new { CaptchaId = captchaId, Captcha = captcha }) > 0) return captcha64;
        return "Failed to generate captcha! Please try again";
    }
}