using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class LinkRepository : GenericRepository<Link>, ILinkRepository
    {
        public LinkRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }
    }
}