using System.Text;
using fly_server.Helpers;
using fly_server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<Auth>();


// ***** Bearer Token Config *****

string tokenKey = builder.Configuration.GetSection("AppSettings:TokenKey").Value;

SymmetricSecurityKey encTokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
TokenValidationParameters tokenValidationParameters = new()
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = encTokenKey,
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParameters;
});

// ***** End of JWT Config *****

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
