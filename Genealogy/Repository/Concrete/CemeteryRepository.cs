using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class CemeteryRepository : GenericRepository<Cemetery>, ICemeteryRepository
    {
        public CemeteryRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }
    }
}