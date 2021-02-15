using Genealogy.Models;
using Genealogy.Repository.Abstract;

namespace Genealogy.Repository.Concrete
{
    public class BusinessObjectRepository : GenericRepository<BusinessObject>, IBusinessObjectRepository
    {
        public BusinessObjectRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        {}
    }
}