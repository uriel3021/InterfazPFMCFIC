using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using InterfazPFMCFIC.Specifications;
using InterfazPFMCFIC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SeguimientoSolicitudModel : PageModel
{
    private readonly IRepositoryBase<InterfazPfmCficArchivo> _repoArchivo;
    private readonly IRepositoryBase<InterfazPfmCficSolicitud> _repoSolicitud;
    private readonly IConfiguration _configuration;

    public SeguimientoSolicitudModel(
        IRepositoryBase<InterfazPfmCficArchivo> repoArchivo, 
        IRepositoryBase<InterfazPfmCficSolicitud> repoSolicitud,
        IConfiguration configuration)
    {
        _repoArchivo = repoArchivo;
        _repoSolicitud = repoSolicitud;
        _configuration = configuration;

    }

    [BindProperty(SupportsGet = true)]
    public int ActoID { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Pagina { get; set; } = 1;

    public int RegistrosPorPagina { get; set; } = 5;
    public int TotalPaginas { get; set; }

    public List<ConfirmacionEnvioTablaViewModel> Confirmaciones { get; set; } = new();

    public async Task OnGetAsync()
    {
        // Obtener todos los registros para el ActoID
        var spec = new ConfirmacionSolicitudPorActoIdSpec(ActoID);
        var solicitud = await _repoSolicitud.ListAsync(spec);

        //Obtengo el archivo asociado
       // var specArchivo = new ConfirmacionArchivoPorIdSpec(solicitud.FirstOrDefault().SolicitudPfmcficid);
        //var archivo = await _repoArchivo.FirstOrDefaultAsync(specArchivo);

        // Calcular paginación
        int totalRegistros = solicitud.Count;
        TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina);

        // Obtener solo los registros de la página actual
        var paginaActual = solicitud
            .Skip((Pagina - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
            .ToList();

        // Para cada solicitud, busca su archivo asociado
        // ...
        Confirmaciones = new List<ConfirmacionEnvioTablaViewModel>();
        foreach (var e in paginaActual)
        {
            var specArchivo = new ConfirmacionArchivoPorIdSpec(e.SolicitudPfmcficid);
            var archivo = await _repoArchivo.FirstOrDefaultAsync(specArchivo);

            string? nombre = null;
            string? extension = null;
            if (archivo != null && !string.IsNullOrEmpty(archivo.NombreArchivo))
            {
                nombre = Path.GetFileNameWithoutExtension(archivo.NombreArchivo);
                extension = Path.GetExtension(archivo.NombreArchivo);
            }

            Confirmaciones.Add(new ConfirmacionEnvioTablaViewModel
            {
                Estatus = e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault()?.TipoConfirmacion.HasValue == true
                    ? (EstatusEnvio?)e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault().TipoConfirmacion.Value
                    : null,
                Fecha = e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault()?.FechaRegistro,
                Folio = e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault()?.FolioConfirmacionCfic,
                ArchivoId = archivo?.ArchivoId ?? 0,
                ArchivoNombre = nombre,
                ArchivoExtension = extension
            });
        }

    }

    public async Task<IActionResult> OnGetDescargarArchivoAsync(int archivoId, long ticks)
    {
        var archivo = await _repoArchivo.GetByIdAsync(archivoId);
        if (archivo == null || string.IsNullOrEmpty(archivo.Ruta))
            return NotFound();

        var rutaBase = _configuration["Archivos:RutaBase"];
        var extension = _configuration["Archivos:Extension"];

        // Solo agrega la extensión si no la tiene
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

        return PhysicalFile(rutaFisica, contentType, nombreArchivo);
    }

    /*
    public async Task<IActionResult> OnGetDescargarArchivo()
    {
       return Content($"Handler invocado correctamente con archivoId");
    }
    */
}