using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class CountyRepository : GenericRepository<County>, ICountyRepository
    {
        public CountyRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }
    }
}