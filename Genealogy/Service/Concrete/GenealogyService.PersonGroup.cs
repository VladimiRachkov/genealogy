using System;
using System.Collections.Generic;
using System.Linq;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Genealogy.Service.Helpers;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public PersonGroup CreatePersonGroup()
        {
            var id = Guid.NewGuid();
            var personGroup = new PersonGroup(id);
            PersonGroup result = null;

            if (_unitOfWork.PersonGroupRepository.Add(personGroup))
            {
                result = _unitOfWork.PersonGroupRepository.GetByID(id);
            }

            return result;
        }
    }
}