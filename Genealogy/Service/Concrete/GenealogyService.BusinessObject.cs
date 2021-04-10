using System;
using System.Linq;
using System.Collections.Generic;
using Genealogy.Models;
using Genealogy.Service.Astract;
using Genealogy.Helpers;
using Genealogy.Service.Helpers;
using Genealogy.Data;
using Newtonsoft.Json;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<BusinessObjectOutDto> GetBusinessObjects(BusinessObjectFilter filter)
        {
            var businessObjects = _unitOfWork.BusinessObjectRepository.Get(
            x =>
                (filter.Id != null ? x.Id == filter.Id : true) &&
                (filter.Name != null ? x.Name == filter.Name : true) &&
                (filter.MetatypeId != null ? x.Metatype.Id == filter.MetatypeId : true),
            x =>
                x.OrderBy(item => item.Name).ThenBy(item => item.Id), "Metatype");

            if (filter.Step > 0)
            {
                businessObjects = businessObjects.Where((item, index) => index >= filter.Step * filter.Index && index < (filter.Step * filter.Index) + filter.Step);
            }

            return businessObjects.Select(i => _mapper.Map<BusinessObjectOutDto>(i)).ToList();
        }

        public BusinessObjectOutDto CreateBusinessObjectsFromDto(BusinessObjectInDto boDto)
        {
            var bo = _mapper.Map<BusinessObject>(boDto);
            var result = createBusinessObject(bo);

            SendMessage(boDto);

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

                if (boDto.IsRemoved != null && boDto.IsRemoved.Value && bo.IsRemoved)
                {
                    result = RemoveBusinessObject(bo) ? _mapper.Map<BusinessObjectOutDto>(bo) : null;
                }
                else
                {
                    if (boDto.Data != null)
                    {
                        bo.Data = boDto.Data;
                    }

                    if (boDto.Name != null)
                    {
                        bo.Name = boDto.Name;
                    }

                    if (boDto.Title != null)
                    {
                        bo.Title = boDto.Title;
                    }

                    if (boDto.IsRemoved != null)
                    {
                        if (boDto.IsRemoved.Value == true)
                        {
                            bo.FinishDate = DateTime.Now;
                            bo.IsRemoved = true;
                        }
                        else
                        {
                            bo.IsRemoved = false;
                        }
                    }

                    _unitOfWork.BusinessObjectRepository.Update(bo);
                    _unitOfWork.Save();

                    result = _mapper.Map<BusinessObjectOutDto>(bo);
                }

                if (result != null)
                {
                    result = _mapper.Map<BusinessObjectOutDto>(result);
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

        private BusinessObject createBusinessObject(BusinessObject bo)
        {
            bo.Id = Guid.NewGuid();
            bo.StartDate = DateTime.Now;


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


    }
}