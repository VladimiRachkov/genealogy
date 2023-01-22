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
        public List<CountyDto> GetCounty(CountyFilter filter)
        {
            return _unitOfWork.CountyRepository.Get(x =>
            (filter.Id != Guid.Empty ? x.Id == filter.Id : true)).Select(i => _mapper.Map<CountyDto>(i)).ToList();
        }
        public CountyDto AddCounty(CountyDto newCounty)
        {
            if (newCounty != null)
            {
                var county = _mapper.Map<County>(newCounty);
                var id = Guid.NewGuid();
                county.Id = id;
                _unitOfWork.CountyRepository.Add(county);
                _unitOfWork.Save();

                var result = _unitOfWork.CountyRepository.GetByID(id);
                return _mapper.Map<CountyDto>(result);
            }
            return null;
        }

        protected County addCounty(string name)
        {
            var id = Guid.NewGuid();
            var county = new County()
            {
                Id = id,
                Name = name,
                isRemoved = false
            };

            _unitOfWork.CountyRepository.Add(county);
            _unitOfWork.Save();

            return _unitOfWork.CountyRepository.GetByID(id);
        }


        public List<CountyDto> GetCountyList()
        {
            return _unitOfWork.CountyRepository.Get().Select(i => _mapper.Map<CountyDto>(i)).ToList();
        }

        public CountyDto ChangeCounty(CountyDto countyDto)
        {
            if (countyDto != null && countyDto.Id != null)
            {
                var county = _mapper.Map<County>(countyDto);
                var result = UpdateCounty(county);
                return _mapper.Map<CountyDto>(result);
            }
            return null;
        }

        private County UpdateCounty(County county)
        {
            _unitOfWork.CountyRepository.Update(county);
            _unitOfWork.Save();
            return _unitOfWork.CountyRepository.GetByID(county.Id);
        }

        public CountyDto RemoveCounty(Guid countyId)
        {
            if (countyId != Guid.Empty)
            {
                var county = _unitOfWork.CountyRepository.GetByID(countyId);
                if (county != null)
                {
                    if (!county.isRemoved)
                    {
                        var cemeteries = getCemeteriesByCountyId(countyId);
                        cemeteries.ToList().ForEach(item => item.isRemoved = true);
                        updateCemeteries(cemeteries);

                        county.isRemoved = true;
                        county = UpdateCounty(county);

                    }
                    else
                    {
                        removeCemeteriesByCountyId(countyId);
                        removeCounty(county);
                    }
                }
                return _mapper.Map<CountyDto>(county);
            }
            return null;
        }

        private void removeCounty(County county)
        {
            _unitOfWork.CountyRepository.Delete(county);
            _unitOfWork.Save();
        }

        public CountyDto RestoreCounty(Guid countyId)
        {
            if (countyId != Guid.Empty)
            {
                var county = _unitOfWork.CountyRepository.GetByID(countyId);
                if (county != null && county.isRemoved)
                {
                    var cemeteries = getCemeteriesByCountyId(countyId);
                    cemeteries.ToList().ForEach(item => item.isRemoved = false);
                    updateCemeteries(cemeteries);

                    county.isRemoved = false;
                    county = UpdateCounty(county);
                }
                return _mapper.Map<CountyDto>(county);
            }
            return null;
        }
    }
}