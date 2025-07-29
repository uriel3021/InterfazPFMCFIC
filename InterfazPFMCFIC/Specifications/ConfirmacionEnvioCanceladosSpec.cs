using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class ConfirmacionEnvioCanceladosSpec : Specification<InterfazPfmCficConfirmacionEnvio>
{
    public ConfirmacionEnvioCanceladosSpec(int solicitudPfmcficid, int tipoCanceladoId)
    {
        Query.Where(x =>
            x.SolictudPfmcficid == solicitudPfmcficid &&
            x.TipoConfirmacion == tipoCanceladoId &&
            x.CodigoRetorno == 1
        );
    }
}