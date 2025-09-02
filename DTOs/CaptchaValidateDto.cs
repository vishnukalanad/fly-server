namespace fly_server.DTOs;

public partial class CaptchaValidateDto
{
    public string Captcha { get; set; } = "";
    public string CaptchaId { get; set; } = "";
}