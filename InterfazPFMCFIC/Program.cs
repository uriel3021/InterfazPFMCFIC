using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura la autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "tu-issuer", // Cambia esto por el issuer de tu JWT
            ValidAudience = "tu-audience", // Cambia esto por el audience de tu JWT
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu-clave-secreta-super-segura")) // Cambia esto por tu clave secreta
        };
    });

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();