using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class MetatypeRepository : GenericRepository<Metatype>, IMetatypeRepository
    {
        public MetatypeRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }
    }
}