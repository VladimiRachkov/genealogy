using System;
using System.Collections.Generic;
using System.Linq;
using Genealogy.Models;
using Genealogy.Repository.Concrete;
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

        protected Cemetery addCemetery(string name)
        {
            var id = Guid.NewGuid();
            var cemetery = new Cemetery()
            {
                Id = id,
                Name = name,
                Location = null,
                isRemoved = false
            };

            _unitOfWork.CemeteryRepository.Add(cemetery);
            _unitOfWork.Save();

            return _unitOfWork.CemeteryRepository.GetByID(id);
        }


        public List<CemeteryDto> GetCemeteryList()
        {
            return _unitOfWork.CemeteryRepository.Get().Select(i => _mapper.Map<CemeteryDto>(i)).ToList();
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

        public CemeteryDto RemoveCemetery(Guid cemeteryId)
        {
            if (cemeteryId != Guid.Empty)
            {
                var cemetery = _unitOfWork.CemeteryRepository.GetByID(cemeteryId);
                if (cemetery != null)
                {
                    if (!cemetery.isRemoved)
                    {
                        var persons = getPersonByCemeteryId(cemeteryId);
                        persons.ToList().ForEach(item => item.isRemoved = true);
                        updatePersons(persons);

                        cemetery.isRemoved = true;
                        cemetery = UpdateCemetery(cemetery);

                    }
                    else
                    {
                        removePersonsByCemeteryId(cemeteryId);
                        removeCemetery(cemetery);
                    }
                }
                return _mapper.Map<CemeteryDto>(cemetery);
            }
            return null;
        }

        private void removeCemetery(Cemetery cemetery)
        {
            _unitOfWork.CemeteryRepository.Delete(cemetery);
            _unitOfWork.Save();
        }

        public CemeteryDto RestoreCemetery(Guid cemeteryId)
        {
            if (cemeteryId != Guid.Empty)
            {
                var cemetery = _unitOfWork.CemeteryRepository.GetByID(cemeteryId);
                if (cemetery != null && cemetery.isRemoved)
                {
                    var persons = getPersonByCemeteryId(cemeteryId);
                    persons.ToList().ForEach(item => item.isRemoved = false);
                    updatePersons(persons);

                    cemetery.isRemoved = false;
                    cemetery = UpdateCemetery(cemetery);
                }
                return _mapper.Map<CemeteryDto>(cemetery);
            }
            return null;
        }
    }
}