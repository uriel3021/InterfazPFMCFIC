using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class RechazoSpecification : Specification<InterfazPfmCficRechazo>
{
    public RechazoSpecification(int solicitudId)
    {
        Query.Where(r => r.SolictudPfmcficid == solicitudId);
    }
}