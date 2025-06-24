using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class CancelacionPorSolicitudIdSpec : Specification<InterfazPfmCficCancelacione>
{
    public CancelacionPorSolicitudIdSpec(int solicitudPfmcficid)
    {
        Query.Where(x => x.SolictudPfmcficid == solicitudPfmcficid);
    }
}