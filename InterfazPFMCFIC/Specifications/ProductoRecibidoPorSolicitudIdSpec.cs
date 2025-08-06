using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;

public class ProductoRecibidoPorSolicitudIdSpec : Specification<InterfazPfmCficProductorecibido>
{
    public ProductoRecibidoPorSolicitudIdSpec(int solicitudPfmcficid, int tipoConfirmacion)
    {
        Query.Where(x => x.SolicitudPfmcficid == solicitudPfmcficid && x.CatTipoConfirmacionId == tipoConfirmacion);
    }
}