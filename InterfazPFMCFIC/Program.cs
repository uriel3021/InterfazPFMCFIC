using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using InterfazPFMCFIC.Infrastructure;
using InterfazPFMCFIC.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

// Configura CORS para permitir peticiones desde la app externa
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7197") // Cambia por la URL de tu app externa
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configura el DbContext usando la cadena de conexión del archivo de configuración
builder.Services.AddDbContext<DbInterfazPfmcficContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(EfRepository<>));
builder.Services.AddScoped<PlantillaService>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.Run();