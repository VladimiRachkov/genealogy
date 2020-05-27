using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }
    }
}