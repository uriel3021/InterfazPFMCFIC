using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using InterfazPFMCFIC.Specifications;
using InterfazPFMCFIC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class SeguimientoSolicitudModel : PageModel
{
    private readonly IRepositoryBase<InterfazPfmCficArchivo> _repoArchivo;
    private readonly IRepositoryBase<InterfazPfmCficSolicitud> _repoSolicitud;
    private readonly IRepositoryBase<InterfazPfmCficConfirmacionEnvio> _repoConfirmacionEnvio;
    private readonly IRepositoryBase<InterfazPfmPfmCficConfirmacionRecepcion> _repoConfirmacionRecepcion;
    private readonly IRepositoryBase<InterfazPfmCficRechazo> _repoRechazo;
    private readonly IConfiguration _configuration;

    public SeguimientoSolicitudModel(
        IRepositoryBase<InterfazPfmCficArchivo> repoArchivo,
        IRepositoryBase<InterfazPfmCficSolicitud> repoSolicitud,
        IRepositoryBase<InterfazPfmCficConfirmacionEnvio> repoConfirmacionEnvio,
        IRepositoryBase<InterfazPfmPfmCficConfirmacionRecepcion> repoConfirmacionRecepcion,
        IRepositoryBase<InterfazPfmCficRechazo> repoRechazo,
        IConfiguration configuration)
    {
        _repoArchivo = repoArchivo;
        _repoSolicitud = repoSolicitud;
        _repoConfirmacionEnvio = repoConfirmacionEnvio;
        _repoConfirmacionRecepcion = repoConfirmacionRecepcion;
        _repoRechazo = repoRechazo;
        _configuration = configuration;
    }

    [BindProperty(SupportsGet = true)]
    public int ActoID { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Pagina { get; set; } = 1;

    public int RegistrosPorPagina { get; set; } = 5;
    public int TotalPaginas { get; set; }

    public List<MovimientoTablaViewModel> Movimientos { get; set; } = new();

    public async Task OnGetAsync()
    {
        // Obtener todas las solicitudes para el ActoID
        var spec = new ConfirmacionSolicitudPorActoIdSpec(ActoID);
        var solicitudes = await _repoSolicitud.ListAsync(spec);

        int totalRegistros = solicitudes.Count;
        TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina);

        // Obtener solo los registros de la página actual
        var paginaActual = solicitudes
            .Skip((Pagina - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
            .ToList();

        var movimientos = new List<MovimientoTablaViewModel>();

        foreach (var solicitud in paginaActual)
        {
            // ENVIADOS
            var enviados = await _repoConfirmacionEnvio.ListAsync(
                new ConfirmacionEnvioEnviadosSpec(solicitud.SolicitudPfmcficid));
            foreach (var c in enviados)
            {
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        solicitud.SolicitudPfmcficid,
                        (int)TipoConfirmacion.Enviado));

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = TipoConfirmacion.Enviado,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionCfic,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo
                });
            }

            // ACEPTADOS (NO archivo)
            var aceptadosRecepcion = await _repoConfirmacionRecepcion.ListAsync(
                new ConfirmacionRecepcionAceptadosSpec(solicitud.SolicitudPfmcficid));
            foreach (var c in aceptadosRecepcion)
            {
                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = TipoConfirmacion.Aceptado,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionPfm,
                    ArchivoId = 0,
                    ArchivoNombre = null
                });
            }

            // RECHAZADOS
            var rechazos = await _repoRechazo.ListAsync(
                new RechazoSpecification(solicitud.SolicitudPfmcficid));
            foreach (var r in rechazos)
            {
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        r.RechazoId,
                        (int)TipoConfirmacion.Rechazado));

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = TipoConfirmacion.Rechazado,
                    Fecha = r.FechaEnvio,              
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo,
                    TipoRechazoId = r.TipoRechazoId,
                    MotivoRechazoId = r.MotivoRechazoId,
                    ObservacionesRechazo = r.Observaciones
                });
            }
        }

        // Ordena por fecha descendente (de más reciente a más antigua)
        Movimientos = movimientos
            .OrderByDescending(m => m.Fecha)
            .ToList();
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
}