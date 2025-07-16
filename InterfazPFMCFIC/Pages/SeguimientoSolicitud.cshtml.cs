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

[AllowAnonymous]
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
            }

            // REENVIADOS
            if (solicitud.CatTipoEnvioId == 2)
            {
                var reenviados = await _repoConfirmacionEnvio.ListAsync(
                    new ConfirmacionEnvioReenviadosSpec(solicitud.SolicitudPfmcficid));
                foreach (var c in reenviados)
                {
                    var archivo = await _repoArchivo.FirstOrDefaultAsync(
                        new ArchivoPorRegistroYProcesoSpec(
                            solicitud.SolicitudPfmcficid,
                            (int)TipoConfirmacion.Reenviado));

                    movimientos.Add(new MovimientoTablaViewModel
                    {
                        Tipo = TipoConfirmacion.Reenviado,
                        Fecha = c.FechaRegistro,
                        Folio = c.FolioConfirmacionCfic,
                        ArchivoId = archivo?.ArchivoId ?? 0,
                        ArchivoNombre = archivo?.NombreArchivo
                    });
                }
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

            // ATENDIDO PARCIAL
            var atendidosParciales = await _repoConfirmacionRecepcion.ListAsync(
                new ConfirmacionRecepcionAtendidoParcialSpec(solicitud.SolicitudPfmcficid));
            foreach (var c in atendidosParciales)
            {
                // Obtener el ProductoRecibidoId relacionado a la solicitud 
                var productoRecibido = await _repoProductoRecibido.FirstOrDefaultAsync(
                new ProductoRecibidoPorSolicitudIdSpec(solicitud.SolicitudPfmcficid)
                );

                int productoRecibidoId = productoRecibido?.ProductoRecibidoId ?? 0;

                // Obtener el archivo usando la spec existente
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        productoRecibidoId,
                        (int)TipoConfirmacion.AtendidoParcial
                    )
                );

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = TipoConfirmacion.AtendidoParcial,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionPfm,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo,
                });
            }

            // ATENDIDO TOTAL
            var atendidosTotales = await _repoConfirmacionRecepcion.ListAsync(
                new ConfirmacionRecepcionAtendidoTotalSpec(solicitud.SolicitudPfmcficid));
            foreach (var c in atendidosTotales)
            {
                // Obtener el ProductoRecibidoId relacionado a la solicitud 
                var productoRecibido = await _repoProductoRecibido.FirstOrDefaultAsync(
                new ProductoRecibidoPorSolicitudIdSpec(solicitud.SolicitudPfmcficid)
                );

                int productoRecibidoId = productoRecibido?.ProductoRecibidoId ?? 0;

                // Obtener el archivo usando la spec existente
                var archivo = await _repoArchivo.FirstOrDefaultAsync(
                    new ArchivoPorRegistroYProcesoSpec(
                        productoRecibidoId,
                        (int)TipoConfirmacion.AtendidoTotal
                    )
                );

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = TipoConfirmacion.AtendidoTotal,
                    Fecha = c.FechaRegistro,
                    Folio = c.FolioConfirmacionPfm,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo,
                });
            }

            // CANCELADOS
            var cancelados = await _repoConfirmacionEnvio.ListAsync(
                new ConfirmacionEnvioCanceladosSpec(solicitud.SolicitudPfmcficid)
            );

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
                        cancelacionId,
                        (int)TipoConfirmacion.Cancelado
                    )
                );

                movimientos.Add(new MovimientoTablaViewModel
                {
                    Tipo = TipoConfirmacion.Cancelado,
                    Fecha = fechaCancelacion,
                    Folio = c.FolioConfirmacionCfic,
                    ArchivoId = archivo?.ArchivoId ?? 0,
                    ArchivoNombre = archivo?.NombreArchivo
                });
            }
        }
        Movimientos = movimientos
         .OrderBy(m => m.Fecha)
         .ToList();
        MotivosRechazo = await _repoMotivoRechazo.ListAsync(new MotivosRechazoActivosSpec());

        return Page();
    }
}
