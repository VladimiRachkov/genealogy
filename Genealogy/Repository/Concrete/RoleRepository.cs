
using System;
using System.Linq;
using Genealogy.Models;
using Genealogy.Repository.Abstract;


namespace Genealogy.Repository.Concrete
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(GenealogyContext _GenealogyContext) : base(_GenealogyContext)
        { }

        public Role GetRoleById(Guid roleId)
        {
            return _dbContext.Roles.Where(role => role.Id == roleId).FirstOrDefault();
        }
    }
}