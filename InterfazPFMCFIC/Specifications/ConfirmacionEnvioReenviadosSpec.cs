using Ardalis.Specification;
using InterfazPFMCFIC.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class ConfirmacionEnvioReenviadosSpec : Specification<InterfazPfmCficConfirmacionEnvio>
{
    public ConfirmacionEnvioReenviadosSpec(int solicitudPfmcficid)
    {
        Query.Where(x => x.SolictudPfmcficid == solicitudPfmcficid && x.TipoConfirmacion == (int)TipoConfirmacion.Reenviado);
    }
}