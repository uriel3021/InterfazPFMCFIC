using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;
public class ConfirmacionSolicitudPorActoIdSpec : Specification<InterfazPfmCficSolicitud>
{
    public ConfirmacionSolicitudPorActoIdSpec(int actoId)
    {
        Query
            .Include(x => x.InterfazPfmCficConfirmacionEnvios)
            .Where(c => c.ActoId == actoId);
    }
}