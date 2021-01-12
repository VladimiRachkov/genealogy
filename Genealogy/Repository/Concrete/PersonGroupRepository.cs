using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class PersonGroupRepository : GenericRepository<PersonGroup>, IPersonGroupRepository
    {
        public PersonGroupRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }
    }
}