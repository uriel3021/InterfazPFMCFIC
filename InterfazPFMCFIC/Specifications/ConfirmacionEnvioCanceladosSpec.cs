using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class ConfirmacionEnvioCanceladosSpec : Specification<InterfazPfmCficConfirmacionEnvio>
{
    public ConfirmacionEnvioCanceladosSpec(int solicitudPfmcficid)
    {
        Query.Where(x =>
            x.SolictudPfmcficid == solicitudPfmcficid &&
            x.TipoConfirmacion == (int)TipoConfirmacion.Cancelado &&
            x.CodigoRetorno == 1
        );
    }
}