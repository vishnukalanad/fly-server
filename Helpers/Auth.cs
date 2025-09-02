using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace fly_server.Helpers;

public class Auth
{
    private readonly IConfiguration _config;
    public Auth(IConfiguration config)
    {
        _config = config;
    }


    // Generates the password hash value;
    public byte[] GeneratePasswordHash(string password, out byte[] passwordSalt)
    {
        byte[] salt = new byte[128 / 8];
        using (RandomNumberGenerator gen = RandomNumberGenerator.Create())
        {
            gen.GetNonZeroBytes(salt);
        }
        
        string saltedPassword =_config.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(salt);
        byte[] hash = KeyDerivation.Pbkdf2(password: password, salt: Encoding.ASCII.GetBytes(saltedPassword),
            prf: KeyDerivationPrf.HMACSHA256, iterationCount: 100, numBytesRequested: 256 / 8);
        
        passwordSalt = salt;
        return hash;
    }
}