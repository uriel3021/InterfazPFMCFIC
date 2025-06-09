using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class ConfirmacionRecepcionAceptadosSpec : Specification<InterfazPfmPfmCficConfirmacionRecepcion>
{
    public ConfirmacionRecepcionAceptadosSpec(int solicitudId)
    {
        Query.Where(c =>
            c.SolictudPfmcficid == solicitudId &&
            c.CodigoRetorno == 1 &&
            c.TipoConfirmacion == (int)TipoConfirmacion.Aceptado);
    }
}