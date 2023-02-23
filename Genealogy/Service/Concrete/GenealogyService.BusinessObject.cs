using System;
using System.Linq;
using System.Collections.Generic;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Genealogy.Service.Helpers;
using Genealogy.Data;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<BusinessObjectOutDto> GetBusinessObjectsDto(BusinessObjectFilter filter)
        {
            var businessObjects = GetBusinessObjects(filter);

            if (businessObjects != null)
            {
                return businessObjects.Select(i => _mapper.Map<BusinessObjectOutDto>(i)).ToList();
            }

            return null;
        }

        public IEnumerable<BusinessObject> GetBusinessObjects(BusinessObjectFilter filter)
        {
            var businessObjects = _unitOfWork.BusinessObjectRepository.Get(
            x =>
                (filter.Id != null ? x.Id == filter.Id : true) &&
                (filter.Name != null ? x.Name == filter.Name : true) &&
                (filter.MetatypeId != null ? x.Metatype.Id == filter.MetatypeId : true) &&
                (filter.UserId != null ? x.UserId == filter.UserId : true) &&
                (filter.IsRemoved != null ? x.IsRemoved == filter.IsRemoved : true),
            x =>
                x.OrderBy(item => item.Name).ThenBy(item => item.Id), "Metatype");

            if (filter.Step > 0)
            {
                businessObjects = businessObjects.Where((item, index) => index >= filter.Step * filter.Index && index < (filter.Step * filter.Index) + filter.Step);
            }

            return businessObjects;
        }

        public BusinessObjectOutDto CreateBusinessObjectsFromDto(BusinessObjectInDto boDto)
        {
            var bo = _mapper.Map<BusinessObject>(boDto);
            var result = createBusinessObject(bo);

            SendMessage(boDto);

            return _mapper.Map<BusinessObjectOutDto>(result);
        }

        public BusinessObjectOutDto UpdateBusinessObjectDto(BusinessObjectInDto boDto)
        {
            BusinessObjectOutDto result = null;

            if (boDto != null && boDto.Id != null)
            {
                var changedBO = _mapper.Map<BusinessObject>(boDto);
                var updatedBO = UpdateBusinessObject(changedBO);

                result = _mapper.Map<BusinessObjectOutDto>(updatedBO);
            }

            return result;
        }

        public BusinessObject UpdateBusinessObject(BusinessObject changedBO)
        {
            BusinessObject result = null;

            if (changedBO != null && changedBO.Id != null)
            {
                //TODO: Сделать проверку всех свойств на наличие изменений

                //changedPerson.Cemetery = _unitOfWork.CemeteryRepository.GetByID(personDto.CemeteryId);

                var bo = _unitOfWork.BusinessObjectRepository.GetByID(changedBO.Id);

                if (changedBO.IsRemoved && bo.IsRemoved)
                {
                    result = RemoveBusinessObject(bo) ? _mapper.Map<BusinessObject>(bo) : null;
                }
                else
                {
                    if (changedBO.Data != null)
                    {
                        bo.Data = changedBO.Data;
                    }

                    if (changedBO.Name != null)
                    {
                        bo.Name = changedBO.Name;
                    }

                    if (changedBO.Title != null)
                    {
                        bo.Title = changedBO.Title;
                    }

                    if (changedBO.IsRemoved == true)
                    {
                        bo.FinishDate = DateTime.Now;
                        bo.IsRemoved = true;
                    }
                    else
                    {
                        bo.IsRemoved = false;
                    }

                    _unitOfWork.BusinessObjectRepository.Update(bo);
                    _unitOfWork.Save();

                    result = bo;
                }
            }
            return result;
        }

        public BusinessObjectsCountOutDto GetBusinessObjectsCount(BusinessObjectFilter filter)
        {
            var result = new BusinessObjectsCountOutDto();

            var businessObjects = _unitOfWork.BusinessObjectRepository.Get(
                x =>
                    (filter.MetatypeId != Guid.Empty ? x.Metatype.Id == filter.MetatypeId : true));

            result.count = businessObjects.Count();
            return result;
        }

        private bool RemoveBusinessObject(BusinessObject bo)
        {
            _unitOfWork.BusinessObjectRepository.Delete(bo);
            _unitOfWork.Save();

            return true;
        }

        private BusinessObject createBusinessObject(BusinessObject bo, Guid? userId = null)
        {
            bo.Id = Guid.NewGuid();
            bo.StartDate = DateTime.Now;
            bo.UserId = userId ?? GetCurrentUserId();

            if (bo.Metatype == null)
            {
                if (bo.MetatypeId == null)
                {
                    throw new AppException("Не указан тип объекта.");
                }
                else
                {
                    bo.Metatype = _unitOfWork.MetatypeRepository.GetByID(bo.MetatypeId);
                }
            }

            if (bo.Name == null)
            {
                bo.Name = bo.Title.Trim();
            }
            else
            {
                bo.Name = bo.Name.Trim();
            }

            if (bo.Title == null)
            {
                bo.Title = bo.Name.Trim();
            }
            else
            {
                bo.Title = bo.Title.Trim();
            }

            if (bo.Title == null && bo.Name == null)
            {
                var count = _unitOfWork.BusinessObjectRepository.Count();
                bo.Title = bo.Name = $"{bo.Metatype.Title} {count}";
            }

            _unitOfWork.BusinessObjectRepository.Add(bo);
            _unitOfWork.Save();

            var result = _unitOfWork.BusinessObjectRepository.GetByID(bo.Id);

            return result;
        }

        public BusinessObjectOutDto RemoveBusinessObject(Guid id)
        {
            if (id != null)
            {
                var bo = _unitOfWork.BusinessObjectRepository.GetByID(id);
                _unitOfWork.BusinessObjectRepository.Delete(bo);
                _unitOfWork.Save();
                return _mapper.Map<BusinessObject, BusinessObjectOutDto>(bo);
            }

            return null;
        }


    }
}