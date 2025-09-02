using System.Text;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace fly_server.Helpers;

public class CaptchaGenerator
{
    private static readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    public string GenerateCaptcha(out string captcha)
    {
        var random = new Random();
        var sb = new StringBuilder();

        for (var i = 0; i < 6; i++)
        {
            sb.Append(chars[random.Next(chars.Length)]);
        }
        
        string captchaString = sb.ToString();
        
        using var image = new Image<Rgba32>(150, 50, SixLabors.ImageSharp.Color.White);

        var fontCollection = new FontCollection();
        var fontFam = fontCollection.Add("Assets/STFANGSO.TTF");
        var font = fontFam.CreateFont(24, FontStyle.Bold);
        
        image.Mutate(ctx =>
        {
            ctx.DrawText(captchaString, font, SixLabors.ImageSharp.Color.Black, new PointF(10, 10));
        });
        
        using var stream = new MemoryStream();
        image.SaveAsPng(stream);
        var base64 = Convert.ToBase64String(stream.ToArray());
        captcha = captchaString;
        return $"data:image/png;base64,{base64}";
    }
}