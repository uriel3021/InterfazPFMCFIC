using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;
public class ConfirmacionSolicitudPorActoIdSpec : Specification<InterfazPfmCficSolicitud>
{
    public ConfirmacionSolicitudPorActoIdSpec(int actoId)
    {
        Query
         
            .Where(c => c.ActoId == actoId);
    }
}