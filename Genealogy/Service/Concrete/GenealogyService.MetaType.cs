using Genealogy.Models;
using Genealogy.Service.Astract;
using System.Collections.Generic;
using System.Linq;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<MetatypeOutDto> GetMetatypes(MetatypeFilter filter)
        {
            var metatypes = _unitOfWork.MetatypeRepository.Get().Select(i => _mapper.Map<MetatypeOutDto>(i)).ToList();
            return metatypes;
        }
    }
}