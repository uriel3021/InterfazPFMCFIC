using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;

public class PlantillaPorTipoProductoSpec : Specification<InterfazPfmCficPlantilla>
{
    public PlantillaPorTipoProductoSpec(int catTipoProductoId)
    {
        Query.Where(x => x.CatTipoProductoId == catTipoProductoId && (x.Borrado == null || x.Borrado == false));
    }
}
