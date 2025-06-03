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

    public SeguimientoSolicitudModel(IRepositoryBase<InterfazPfmCficArchivo> repoArchivo, IRepositoryBase<InterfazPfmCficSolicitud> repoSolicitud)
    {
        _repoArchivo = repoArchivo;
        _repoSolicitud = repoSolicitud;
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
        Confirmaciones = new List<ConfirmacionEnvioTablaViewModel>();
        foreach (var e in paginaActual)
        {
            var specArchivo = new ConfirmacionArchivoPorIdSpec(e.SolicitudPfmcficid);
            var archivo = await _repoArchivo.FirstOrDefaultAsync(specArchivo);

            Confirmaciones.Add(new ConfirmacionEnvioTablaViewModel
            {
                Estatus = e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault()?.TipoConfirmacion.HasValue == true
                    ? (EstatusEnvio?)e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault().TipoConfirmacion.Value
                    : null,
                Fecha = e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault()?.FechaRegistro,
                Folio = e.InterfazPfmCficConfirmacionEnvios.FirstOrDefault()?.FolioConfirmacionCfic,
                ArchivoId = archivo?.ArchivoId ?? 0,
                Archivo = archivo != null ? Path.GetFileName(archivo.Ruta) : null
            });
        }

    }

    public async Task<IActionResult> OnGetDescargarArchivoAsync(int archivoId)
    {
        var archivo = await _repoArchivo.GetByIdAsync(archivoId);
        if (archivo == null || string.IsNullOrEmpty(archivo.Ruta) || !System.IO.File.Exists(archivo.Ruta))
            return NotFound();

        var contentType = "application/pdf";
        var nombreArchivo = archivo.NombreArchivo ?? Path.GetFileName(archivo.Ruta);
        return PhysicalFile(archivo.Ruta, contentType, nombreArchivo);
    }

    /*
    public async Task<IActionResult> OnGetDescargarArchivo()
    {
       return Content($"Handler invocado correctamente con archivoId");
    }
    */
}