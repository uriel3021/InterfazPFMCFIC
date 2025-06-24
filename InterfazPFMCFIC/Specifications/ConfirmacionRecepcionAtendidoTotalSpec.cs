using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;


public class ConfirmacionRecepcionAtendidoTotalSpec : Specification<InterfazPfmPfmCficConfirmacionRecepcion>
{
    public ConfirmacionRecepcionAtendidoTotalSpec(int solicitudPfmcficid)
    {
        Query.Where(x =>
            x.SolictudPfmcficid == solicitudPfmcficid &&
            x.CodigoRetorno == 1 &&
            x.TipoConfirmacion == (int)TipoConfirmacion.AtendidoTotal
        );
    }
}