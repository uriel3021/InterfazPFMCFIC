using Ardalis.Specification;
using InterfazPFMCFIC.Models;

public class MotivosRechazoActivosSpec : Specification<CatMotivoRechazo>
{
    public MotivosRechazoActivosSpec()
    {
        Query.Where(m => !m.Borrado);
    }
}