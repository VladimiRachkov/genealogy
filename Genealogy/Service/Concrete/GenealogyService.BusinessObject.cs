using System;
using System.Linq;
using System.Collections.Generic;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Genealogy.Helpers;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<BusinessObjectOutDto> GetBusinessObjects(BusinessObjectFilter filter)
        {
            var businessObjects = _unitOfWork.BusinessObjectRepository.Get(
                x =>
                    (filter.Id != Guid.Empty ? x.Id == filter.Id : true) &&
                    (filter.MetatypeId != Guid.Empty ? x.MetatypeId == filter.MetatypeId : true),
                x =>
                    x.OrderBy(item => item.Name));

            return businessObjects.Select(i => _mapper.Map<BusinessObjectOutDto>(i)).ToList();
        }

        public BusinessObjectOutDto CreateBusinessObject(BusinessObjectInDto boDto)
        {
            var newBO = _mapper.Map<BusinessObject>(boDto);
            var id = Guid.NewGuid();

            newBO.Id = id;

            _unitOfWork.BusinessObjectRepository.Add(newBO);
            _unitOfWork.Save();

            var result = _unitOfWork.BusinessObjectRepository.GetByID(id);
            return _mapper.Map<BusinessObjectOutDto>(result);
        }

        public BusinessObjectOutDto UpdateBusinessObject(BusinessObjectInDto boDto)
        {
            BusinessObjectOutDto result = null;

            if (boDto != null && boDto.Id != null)
            {
                //TODO: Сделать проверку всех свойств на наличие изменений
                var updatedBO = _mapper.Map<BusinessObject>(boDto);
                //changedPerson.Cemetery = _unitOfWork.CemeteryRepository.GetByID(personDto.CemeteryId);

                var bo = _unitOfWork.BusinessObjectRepository.GetByID(boDto.Id);

                if (boDto.IsRemoved.Value && bo.IsRemoved)
                {
                    result = RemoveBusinessObject(bo) ? _mapper.Map<BusinessObjectOutDto>(bo) : null;
                }
                else
                {
                    ObjectValues.CopyValues(bo, updatedBO);

                    _unitOfWork.BusinessObjectRepository.Update(bo);
                    _unitOfWork.Save();

                    result = _mapper.Map<BusinessObjectOutDto>(_unitOfWork.PageRepository.GetByID(bo.Id));
                }

                if (result != null)
                {
                    return _mapper.Map<BusinessObjectOutDto>(result);
                }
            }
            return null;
        }

        private bool RemoveBusinessObject(BusinessObject bo)
        {
            _unitOfWork.BusinessObjectRepository.Delete(bo);
            _unitOfWork.Save();

            return true;
        }
    }
}