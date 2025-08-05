using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class RechazoSpecification : Specification<InterfazPfmCficRechazo>
{
    public RechazoSpecification(int solicitudId, int idtipoRechazo)
    {
        Query.Where(r => r.SolictudPfmcficid == solicitudId && r.Borrado == false)
           .Include(x => x.SolictudPfmcfic)
           .Where(x =>
               x.SolictudPfmcfic != null &&
               x.SolictudPfmcfic.InterfazPfmPfmCficConfirmacionRecepcions.Any(c =>
                   c.CodigoRetorno == 1 &&
                   c.TipoConfirmacion == idtipoRechazo
               )
           );
    }
}


