using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;
public class ArchivoPorRegistroYProcesoSpec : Specification<InterfazPfmCficArchivo>
{
    public ArchivoPorRegistroYProcesoSpec(int registroId, int procesoId)
    {
        Query.Where(a => a.RegistroId == registroId && a.CatTipoProcesoId == procesoId);
    }
}