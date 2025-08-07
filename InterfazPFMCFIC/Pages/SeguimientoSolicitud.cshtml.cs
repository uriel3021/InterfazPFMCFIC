using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using InterfazPFMCFIC.Specifications;
using InterfazPFMCFIC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[AllowAnonymous]
// <- Con esto deshabilitas por completo el antiforgery para esta página
[IgnoreAntiforgeryToken]
public class SeguimientoSolicitudModel : PageModel
{
    private readonly IRepositoryBase<InterfazPfmCficArchivo> _repoArchivo;
    private readonly IRepositoryBase<InterfazPfmCficSolicitud> _repoSolicitud;
    private readonly IRepositoryBase<InterfazPfmCficConfirmacionEnvio> _repoConfirmacionEnvio;
    private readonly IRepositoryBase<InterfazPfmPfmCficConfirmacionRecepcion> _repoConfirmacionRecepcion;
    private readonly IRepositoryBase<InterfazPfmCficRechazo> _repoRechazo;
    private readonly IRepositoryBase<InterfazPfmCficProductorecibido> _repoProductoRecibido;
    private readonly IRepositoryBase<InterfazPfmCficCancelacione> _repoCancelacion;
    private readonly IRepositoryBase<MotivoRechazo> _repoMotivoRechazo;
    private readonly IRepositoryBase<InterfazPfmCficCatTipoConfirmacion> _repoTipoConfirmacion;
    private readonly IRepositoryBase<InterfazPfmCficAsignacione> _repoAsignaciones;
    private readonly IConfiguration _configuration;

    public SeguimientoSolicitudModel(
        IRepositoryBase<InterfazPfmCficArchivo> repoArchivo,
        IRepositoryBase<InterfazPfmCficSolicitud> repoSolicitud,
        IRepositoryBase<InterfazPfmCficConfirmacionEnvio> repoConfirmacionEnvio,
        IRepositoryBase<InterfazPfmPfmCficConfirmacionRecepcion> repoConfirmacionRecepcion,
        IRepositoryBase<InterfazPfmCficRechazo> repoRechazo,
        IRepositoryBase<InterfazPfmCficProductorecibido> repoProductoRecibido,
        IRepositoryBase<InterfazPfmCficCancelacione> repoCancelacion,
        IRepositoryBase<MotivoRechazo> repoMotivoRechazo,
        IRepositoryBase<InterfazPfmCficCatTipoConfirmacion> repoTipoConfirmacion,
        IRepositoryBase<InterfazPfmCficAsignacione> repoAsignaciones,
        IConfiguration configuration)
    {
        _repoArchivo = repoArchivo;
        _repoSolicitud = repoSolicitud;
        _repoConfirmacionEnvio = repoConfirmacionEnvio;
        _repoConfirmacionRecepcion = repoConfirmacionRecepcion;
        _repoRechazo = repoRechazo;
        _repoProductoRecibido = repoProductoRecibido;
        _repoCancelacion = repoCancelacion;
        _repoMotivoRechazo = repoMotivoRechazo;
        _repoTipoConfirmacion = repoTipoConfirmacion;
        _repoAsignaciones = repoAsignaciones;
        _configuration = configuration;
    }

    [BindProperty]
    public int ActoID { get; set; }

    [BindProperty]
    public string Token { get; set; }

    public int Pagina { get; set; } = 1;
    public int RegistrosPorPagina { get; set; } = 5;
    public int TotalPaginas { get; set; }

    public List<MovimientoTablaViewModel> Movimientos { get; set; } = new();
    public List<MotivoRechazo> MotivosRechazo { get; set; } = new();
    public List<InterfazPfmCficCatTipoConfirmacion> CatTipoConfirmacion { get; set; } = new();

    public string TipoDescripcionRechazado =>
       CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.Rechazo).Descripcion;

    // 1) Recibe el POST de la app externa
    public IActionResult OnPost()
    {
        TempData[nameof(ActoID)] = ActoID;
        TempData[nameof(Token)] = Token;
        return RedirectToPage(); // redirige limpio, sin querystring
    }

    // 2) Carga tras el redirect y valida el JWT manualmente
    public async Task<IActionResult> OnGetAsync()
    {
        // Recuperar ActoID y Token de TempData
        if (TempData.ContainsKey(nameof(ActoID)) && TempData.ContainsKey(nameof(Token)))
        {
            ActoID = (int)TempData[nameof(ActoID)];
            Token = TempData[nameof(Token)] as string;
        }
        else
        {
            // No vinieron datos: error o página vacía
            return BadRequest();
        }

        // Validar token JWT
        var jwtSection = _configuration.GetSection("Jwt");
        var key = jwtSection["Key"];
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];

        var handler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };

        try
        {
            handler.ValidateToken(Token, parameters, out _);
        }
        catch
        {
            return Unauthorized();
        }

        // Cargar primero el catálogo de motivos de rechazo
        MotivosRechazo = await _repoMotivoRechazo.ListAsync(new MotivosRechazoActivosSpec());
        CatTipoConfirmacion = await _repoTipoConfirmacion.ListAsync(new CatTipoConfirmacionSpec());

        // Cargar tus Movimientos y MotivosRechazo (tu lógica original)
        var spec = new ConfirmacionSolicitudPorActoIdSpec(ActoID);
        var solicitudes = await _repoSolicitud.ListAsync(spec);

        int totalRegistros = solicitudes.Count;
        TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina);

        var paginaActual = solicitudes
            .Skip((Pagina - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
            .ToList();

        var movimientos = new List<MovimientoTablaViewModel>();

        var list = new List<MovimientoTablaViewModel>();

        foreach (var solicitud in paginaActual)
        {
            // ENVIADOS
            if (solicitud.CatTipoEnvioId == 1)
            {
                var tipoEnviado = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.Solicitud);

                var enviados = await _repoConfirmacionEnvio.ListAsync(
                    new ConfirmacionEnvioEnviadosSpec(solicitud.SolicitudPfmcficid, tipoEnviado.CatTipoConfirmacionId));

                foreach (var c in enviados)
                {
                    var archivo = await _repoArchivo.FirstOrDefaultAsync(
                        new ArchivoPorRegistroYProcesoSpec(
                            solicitud.SolicitudPfmcficid,
                             tipoEnviado.CatTipoConfirmacionId));

                    movimientos.Add(new MovimientoTablaViewModel
                    {
                        Tipo = tipoEnviado.Descripcion,
                        Fecha = c.FechaRegistro,
                        Folio = c.FolioConfirmacionCfic,
                        ArchivoId = archivo?.ArchivoId ?? 0,
                        ArchivoNombre = archivo?.NombreArchivo
                    });
                }
            }

            // REENVIADOS
            if (solicitud.CatTipoEnvioId == 2)
            {
                var tipoReenviado = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.Actualizacion);

                var reenviados = await _repoConfirmacionEnvio.ListAsync(
                    new ConfirmacionEnvioReenviadosSpec(solicitud.SolicitudPfmcficid, tipoReenviado.CatTipoConfirmacionId));
               
                foreach (var c in reenviados)
                {
                    var archivo = await _repoArchivo.FirstOrDefaultAsync(
                        new ArchivoPorRegistroYProcesoSpec(
                            solicitud.SolicitudPfmcficid, tipoReenviado.CatTipoConfirmacionId));

                    movimientos.Add(new MovimientoTablaViewModel
                    {
                        Tipo = tipoReenviado.Descripcion,
                        Fecha = c.FechaRegistro,
                        Folio = c.FolioConfirmacionCfic,
                        ArchivoId = archivo?.ArchivoId ?? 0,
                        ArchivoNombre = archivo?.NombreArchivo
                    });
                }
            }

            // ACEPTADOS (NO archivo)
            var tipoAceptacion = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.Aceptacion);

            var aceptadosRecepcion = await _repoConfirmacionRecepcion.ListAsync(
                new ConfirmacionRecepcionAceptadosSpec(solicitud.SolicitudPfmcficid, tipoAceptacion.CatTipoConfirmacionId));

            foreach (var c in aceptadosRecepcion)
            {
                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = tipoAceptacion.Descripcion,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionPfm,
                    ArchivoId = 0,
                    ArchivoNombre = null
                });
            }

            // RECHAZADOS
            var tipoRechazo = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.Rechazo);

            var rechazos = await _repoRechazo.ListAsync(
                new RechazoSpecification(solicitud.SolicitudPfmcficid, tipoRechazo.CatTipoConfirmacionId));

            foreach (var r in rechazos)
            {
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        r.RechazoId, tipoRechazo.CatTipoConfirmacionId));

                var motivoTexto = MotivosRechazo.FirstOrDefault(m => m.MotivoRechazoId == r.MotivoRechazoId)?.Motivo;
                var tipoRechazoTexto = r.TipoRechazoId.HasValue ? ((TipoRechazo)r.TipoRechazoId.Value).ToString() : null;

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = tipoRechazo.Descripcion,
                    Fecha = r.FechaEnvio,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo,
                    TipoRechazoId = r.TipoRechazoId,
                    MotivoRechazoId = r.MotivoRechazoId,
                    ObservacionesRechazo = r.Observaciones,
                    MotivoRechazoTexto = motivoTexto,
                    TipoRechazoTexto = tipoRechazoTexto
                });
            }

            // ATENDIDO PARCIAL
            var tipoAtendidoParcial = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.InformeParcial);

            var atendidosParciales = await _repoConfirmacionRecepcion.ListAsync(
                new ConfirmacionRecepcionAtendidoParcialSpec(solicitud.SolicitudPfmcficid, tipoAtendidoParcial.CatTipoConfirmacionId));

            foreach (var c in atendidosParciales)
            {
                // Obtener el ProductoRecibidoId relacionado a la solicitud 
                var productoRecibido = await _repoProductoRecibido.FirstOrDefaultAsync(
                new ProductoRecibidoPorSolicitudIdSpec(solicitud.SolicitudPfmcficid, tipoAtendidoParcial.CatTipoConfirmacionId)
                );

                int productoRecibidoId = productoRecibido?.ProductoRecibidoId ?? 0;

                // Obtener el archivo usando la spec existente
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        productoRecibidoId, tipoAtendidoParcial.CatTipoConfirmacionId));

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = tipoAtendidoParcial.Descripcion,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionPfm,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo,
                });
            }

            // ATENDIDO TOTAL
            var tipoAtendidoTotal = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.InformeTotal);

            var atendidosTotales = await _repoConfirmacionRecepcion.ListAsync(
                new ConfirmacionRecepcionAtendidoTotalSpec(solicitud.SolicitudPfmcficid, tipoAtendidoTotal.CatTipoConfirmacionId));

            foreach (var c in atendidosTotales)
            {
                // Obtener el ProductoRecibidoId relacionado a la solicitud 
                var productoRecibido = await _repoProductoRecibido.FirstOrDefaultAsync(
                new ProductoRecibidoPorSolicitudIdSpec(solicitud.SolicitudPfmcficid, tipoAtendidoTotal.CatTipoConfirmacionId)
                );

                int productoRecibidoId = productoRecibido?.ProductoRecibidoId ?? 0;

                // Obtener el archivo usando la spec existente
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        productoRecibidoId, tipoAtendidoTotal.CatTipoConfirmacionId));

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = tipoAtendidoTotal.Descripcion,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionPfm,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo,
                });
            }

            // CANCELADOS
            var tipoCancelado = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.Cancelacion);

            var cancelados = await _repoConfirmacionEnvio.ListAsync(
                new ConfirmacionEnvioCanceladosSpec(solicitud.SolicitudPfmcficid, tipoCancelado.CatTipoConfirmacionId));

            foreach (var c in cancelados)
            {
                // Obtener la cancelación relacionada usando especificación
                var cancelacion = await _repoCancelacion.FirstOrDefaultAsync(
                    new CancelacionPorSolicitudIdSpec(solicitud.SolicitudPfmcficid)
                );

                int cancelacionId = cancelacion?.CancelacionId ?? 0;
                DateTime? fechaCancelacion = cancelacion?.FechaCancelacion;

                // Obtener el archivo usando la spec existente
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        cancelacionId, tipoCancelado.CatTipoConfirmacionId));

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = tipoCancelado.Descripcion,
                    Fecha = fechaCancelacion,
                    Folio = c.FolioConfirmacionCfic,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo
                });
            }

            // ASIGNACIONES

            var tipoAsignado = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.NotificacionAsignacion);

            var asignados = await _repoAsignaciones.ListAsync(
                new ConfirmacionRecepcionAsignadosSpec(solicitud.SolicitudPfmcficid, tipoAsignado.CatTipoConfirmacionId));

            foreach (var c in asignados)
            {
                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = tipoAsignado.Descripcion,
                    Fecha = c.FechaAltaDelta,
                    FechaAsignacion = c.FechaAsignacion,
                    NombreAnalista = c.NombreAnalista,
                });
            }

            // AUTO ACEPTACION

            var tipoAutoaceptacion = CatTipoConfirmacion.First(x => x.CatTipoConfirmacionId == (int)TipoConfirmacion.Autoaceptacion);

            var autoAceptados = await _repoConfirmacionRecepcion.ListAsync(
                new ConfirmacionRecepcionAutoAceptadosSpec(solicitud.SolicitudPfmcficid, tipoAutoaceptacion.CatTipoConfirmacionId));

            foreach (var c in autoAceptados)
            {
                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = tipoAutoaceptacion.Descripcion,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionPfm,
                    ArchivoId = 0,
                    ArchivoNombre = null
                });
            }


        }
        Movimientos = movimientos
         .OrderByDescending(m => m.Fecha)
         .ToList();

        return Page();
    }
}
