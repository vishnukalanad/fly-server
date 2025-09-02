namespace fly_server.Models;

public partial class LoginInstanceModel
{
    public string Email { get; set; } = "";
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
}