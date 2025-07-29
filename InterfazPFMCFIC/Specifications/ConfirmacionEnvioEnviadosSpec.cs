using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class ConfirmacionEnvioEnviadosSpec : Specification<InterfazPfmCficConfirmacionEnvio>
{
    public ConfirmacionEnvioEnviadosSpec(int solicitudId, int tipoEnviadoId)
    {
        Query.Where(c =>
            c.SolictudPfmcficid == solicitudId &&
            c.CodigoRetorno == 1 &&
            c.TipoConfirmacion == tipoEnviadoId);
    }
}