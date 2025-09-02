using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace fly_server.Helpers;

public class Auth
{
    private readonly IConfiguration _config;

    public Auth(IConfiguration config)
    {
        _config = config;
    }


    // Generates the password hash and salt value;
    public byte[] GeneratePasswordHashAndSalt(string password, out byte[] passwordSalt)
    {
        byte[] salt = new byte[128 / 8];
        using (RandomNumberGenerator gen = RandomNumberGenerator.Create())
        {
            gen.GetNonZeroBytes(salt);
        }
        byte[] hash = GeneratePasswordHash(password, salt);
        passwordSalt = salt;
        return hash;
    }
    // Generates password hash;
    public byte[] GeneratePasswordHash(string password, byte[] salt)
    {
        string saltedPassword = _config.GetSection("AppSettings:PasswordKey").Value + Convert.ToBase64String(salt);
        byte[] hash = KeyDerivation.Pbkdf2(password: password, salt: Encoding.ASCII.GetBytes(saltedPassword),
            prf: KeyDerivationPrf.HMACSHA256, iterationCount: 100, numBytesRequested: 256 / 8);
        return hash;
    }
    // Generates token string;
    public string GenerateToken(int userId)
    {
        Claim[] claims = [
            new Claim("userId", userId.ToString())
        ];
        
        string tokenKey = _config.GetSection("AppSettings:TokenKey").Value;
        SymmetricSecurityKey encTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
        SigningCredentials cred = new SigningCredentials(encTokenKey, SecurityAlgorithms.HmacSha512Signature);

        SecurityTokenDescriptor desc = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = cred,
            Expires = DateTime.Now.AddHours(1),
        };
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(desc);
        return tokenHandler.WriteToken(token);
    }
}