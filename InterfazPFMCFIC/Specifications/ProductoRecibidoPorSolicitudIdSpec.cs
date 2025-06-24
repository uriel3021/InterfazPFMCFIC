using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;

public class ProductoRecibidoPorSolicitudIdSpec : Specification<InterfazPfmCficProductorecibido>
{
    public ProductoRecibidoPorSolicitudIdSpec(int solicitudPfmcficid)
    {
        Query.Where(x => x.SolicitudPfmcficid == solicitudPfmcficid);
    }
}