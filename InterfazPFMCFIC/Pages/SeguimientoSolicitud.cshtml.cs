using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using InterfazPFMCFIC.Specifications;
using InterfazPFMCFIC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SeguimientoSolicitudModel : PageModel
{
    private readonly IRepositoryBase<InterfazPfmCficConfirmacionEnvio> _repo;
    private readonly IRepositoryBase<InterfazPfmCficSolicitud> _repoSolicitud;

    public SeguimientoSolicitudModel(IRepositoryBase<InterfazPfmCficConfirmacionEnvio> repo, IRepositoryBase<InterfazPfmCficSolicitud> repoSolicitud)
    {
        _repo = repo;
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
        var todas = await _repoSolicitud.ListAsync(spec);

        // Calcular paginación
        int totalRegistros = todas.Count;
        TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina);

        // Obtener solo los registros de la página actual
        var paginaActual = todas
            .Skip((Pagina - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
            .ToList();
        /*
        Confirmaciones = paginaActual.Select(e => new ConfirmacionEnvioTablaViewModel
        {
            Estatus = e.TipoConfirmacion.HasValue ? (EstatusEnvio?)e.TipoConfirmacion.Value : null,
            Fecha = e.FechaRegistro,
            Folio = e.FolioConfirmacionCfic,
            Archivo = ""
        }).ToList();
        */
    }
}