using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class PageRepository : GenericRepository<Page>, IPageRepository
    {
        public PageRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }
    }
}