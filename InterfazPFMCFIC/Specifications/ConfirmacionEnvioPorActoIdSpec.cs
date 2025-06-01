using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;
public class ConfirmacionEnvioPorActoIdSpec : Specification<InterfazPfmCficConfirmacionEnvio>
{
    public ConfirmacionEnvioPorActoIdSpec(int actoId)
    {
        Query.Where(c => c.SolictudPfmcficid == actoId);
    }
}