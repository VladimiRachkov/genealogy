using Genealogy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genealogy.Repository.Abstract
{
    interface IRoleRepository
    {
        Role GetRoleById(Guid roleId);
    }
}