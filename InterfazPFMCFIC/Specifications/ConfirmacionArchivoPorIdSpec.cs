using Ardalis.Specification;
using InterfazPFMCFIC.Models;

namespace InterfazPFMCFIC.Specifications;
public class ConfirmacionArchivoPorIdSpec : Specification<InterfazPfmCficArchivo>
{
    public ConfirmacionArchivoPorIdSpec(int SolicitudPfmcficid)
    {
        Query
  
            .Where(c => c.RegistroId == SolicitudPfmcficid && c.ProcesoId == 1);
    }
}