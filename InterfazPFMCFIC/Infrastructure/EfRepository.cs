namespace InterfazPFMCFIC.Infrastructure;


using Ardalis.Specification.EntityFrameworkCore;
using InterfazPFMCFIC.Models;

public class EfRepository<T> : RepositoryBase<T> where T : class
{
    public EfRepository(DbInterfazPfmcficContext dbContext) : base(dbContext)
    {
    }
}