using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class ConfirmacionRecepcionAutoAceptadosSpec : Specification<InterfazPfmPfmCficConfirmacionRecepcion>
{
    public ConfirmacionRecepcionAutoAceptadosSpec(int solicitudId, int tipoAutoAceptacionId)
    {
        Query.Where(c =>
            c.SolictudPfmcficid == solicitudId &&
            c.CodigoRetorno == 1 &&
            c.TipoConfirmacion == tipoAutoAceptacionId);
    }
}