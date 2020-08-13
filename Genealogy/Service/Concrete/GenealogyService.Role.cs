using System;
using Genealogy.Models;
using Genealogy.Service.Astract;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public Role GetRoleById(Guid roleId)
        {
            return _unitOfWork.RoleRepository.GetRoleById(roleId);
        }

    }
}