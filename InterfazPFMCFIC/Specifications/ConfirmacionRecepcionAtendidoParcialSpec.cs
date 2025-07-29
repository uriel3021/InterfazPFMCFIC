using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;


public class ConfirmacionRecepcionAtendidoParcialSpec : Specification<InterfazPfmPfmCficConfirmacionRecepcion>
{
    public ConfirmacionRecepcionAtendidoParcialSpec(int solicitudPfmcficid, int tipoAtendidoParcialId)
    {
        Query.Where(x =>
            x.SolictudPfmcficid == solicitudPfmcficid &&
            x.CodigoRetorno == 1 &&
            x.TipoConfirmacion == tipoAtendidoParcialId
        );
    }
}