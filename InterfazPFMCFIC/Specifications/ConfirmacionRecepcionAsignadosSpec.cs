using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class ConfirmacionRecepcionAsignadosSpec : Specification<InterfazPfmCficAsignacione>
{
    public ConfirmacionRecepcionAsignadosSpec(int solicitudPfmcficid, int idNotificacionAsignacion)
    {
        Query
            .Where(x => x.SolicitudPfmcficid == solicitudPfmcficid && x.Borrado == false)
            .Include(x => x.SolicitudPfmcfic)
            .Where(x =>
                x.SolicitudPfmcfic != null &&
                x.SolicitudPfmcfic.InterfazPfmPfmCficConfirmacionRecepcions.Any(c =>
                    c.CodigoRetorno == 1 &&
                    c.TipoConfirmacion == idNotificacionAsignacion
                )
            );
    }
}