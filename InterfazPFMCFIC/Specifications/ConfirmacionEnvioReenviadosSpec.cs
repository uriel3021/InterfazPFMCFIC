using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class ConfirmacionEnvioReenviadosSpec : Specification<InterfazPfmCficConfirmacionEnvio>
{
    public ConfirmacionEnvioReenviadosSpec(int solicitudPfmcficid, int tipoReenviadoId)
    {
        Query.Where(x => x.SolictudPfmcficid == solicitudPfmcficid && x.TipoConfirmacion == tipoReenviadoId);
    }
}