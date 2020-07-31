using System;
using System.Collections.Generic;
using System.Linq;
using Genealogy.Models;
using Genealogy.Service.Astract;


namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<CemeteryDto> GetCemetery(CemeteryFilter filter)
        {
            return _unitOfWork.CemeteryRepository.Get(x =>
            (filter.Id != Guid.Empty ? x.Id == filter.Id : true)).Select(i => _mapper.Map<CemeteryDto>(i)).ToList();
        }
        public CemeteryDto AddCemetery(CemeteryDto newCemetery)
        {
            if (newCemetery != null)
            {
                var cemetery = _mapper.Map<Cemetery>(newCemetery);
                var id = Guid.NewGuid();
                cemetery.Id = id;
                _unitOfWork.CemeteryRepository.Add(cemetery);
                _unitOfWork.Save();

                var result = _unitOfWork.CemeteryRepository.GetByID(id);
                return _mapper.Map<CemeteryDto>(result);
            }
            return null;
        }

        public List<CemeteryDto> GetCemeteryList()
        {
            return _unitOfWork.CemeteryRepository.Get().Select(i => _mapper.Map<CemeteryDto>(i)).ToList();
        }

        public CemeteryDto MarkAsRemovedCemetery(Guid id)
        {
            if (id != Guid.Empty)
            {
                var cemetery = _unitOfWork.CemeteryRepository.GetByID(id);
                if (cemetery != null)
                {
                    cemetery.isRemoved = true;
                    var updatedCemetery = UpdateCemetery(cemetery);
                    return _mapper.Map<CemeteryDto>(updatedCemetery);
                }
                return null;
            }
            return null;
        }

        public CemeteryDto ChangeCemetery(CemeteryDto cemeteryDto)
        {
            if (cemeteryDto != null && cemeteryDto.Id != null)
            {
                var cemetery = _mapper.Map<Cemetery>(cemeteryDto);
                var result = UpdateCemetery(cemetery);
                return _mapper.Map<CemeteryDto>(result);
            }
            return null;
        }

        private Cemetery UpdateCemetery(Cemetery cemetery)
        {
            _unitOfWork.CemeteryRepository.Update(cemetery);
            _unitOfWork.Save();
            return _unitOfWork.CemeteryRepository.GetByID(cemetery.Id);
        }
    }
}