using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using InterfazPFMCFIC.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;


public class DescargarArchivoModel : PageModel
{
    private readonly IRepositoryBase<InterfazPfmCficArchivo> _repoArchivo;
    private readonly IConfiguration _configuration;

    public DescargarArchivoModel(
        IRepositoryBase<InterfazPfmCficArchivo> repoArchivo,
        IConfiguration configuration)
    {
        _repoArchivo = repoArchivo;
        _configuration = configuration;
    }

    public async Task<IActionResult> OnGetAsync(int archivoId, string? token = null)
    {
        // Validar el token JWT si se proporciona
        if (!string.IsNullOrEmpty(token))
        {
            if (!await ValidarTokenJWT(token))
            {
                return Unauthorized();
            }
        }
        else
        {
            return Unauthorized();
        }
        
        var archivo = await _repoArchivo.GetByIdAsync(archivoId);
        if (archivo == null || string.IsNullOrEmpty(archivo.Ruta))
            return NotFound();

        var rutaBase = _configuration["Archivos:RutaBase"];
        var extension = _configuration["Archivos:Extension"];

        string rutaRelativa = archivo.Ruta;
        if (!rutaRelativa.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
            rutaRelativa += extension;
        var rutaFisica = Path.Combine(rutaBase, rutaRelativa);

        if (!System.IO.File.Exists(rutaFisica))
            return NotFound();

        var contentType = "application/pdf";
        string nombreArchivo = archivo.NombreArchivo ?? archivo.Ruta;
        if (!nombreArchivo.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
            nombreArchivo += extension;

        Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
        Response.Headers["Pragma"] = "no-cache";
        Response.Headers["Expires"] = "0";
        Response.Headers["Content-Disposition"] = $"inline; filename=\"{nombreArchivo}\"";

        return PhysicalFile(rutaFisica, contentType);
    }

    private async Task<bool> ValidarTokenJWT(string token)
    {
        try
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            
            var tokenHandler = new JsonWebTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = key
            };

            var result = await tokenHandler.ValidateTokenAsync(token, validationParameters);
            return result.IsValid;
        }
        catch
        {
            return false;
        }
    }
}