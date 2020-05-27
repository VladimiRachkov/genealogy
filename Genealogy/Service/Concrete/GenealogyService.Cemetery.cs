using System;
using Genealogy.Models;
using Genealogy.Models.Domain.Dtos;
using Genealogy.Service.Astract;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public Cemetery AddCemetery(CemeteryDto newCemetery)
        {
            var cemetery = _mapper.Map<Cemetery>(newCemetery);
            var id = Guid.NewGuid();
            cemetery.Id = id;
            _unitOfWork.CemeteryRepository.Add(cemetery);
            _unitOfWork.Save();

            return _unitOfWork.CemeteryRepository.GetByID(id);
        }
    }
}