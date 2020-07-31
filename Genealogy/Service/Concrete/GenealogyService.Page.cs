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
        public List<PageDto> GetPage(PageFilter filter)
        {
            return _unitOfWork.PageRepository.Get(x =>
            (filter.Id != Guid.Empty ? x.Id == filter.Id : true) && (filter.Name != null ? x.Name == filter.Name : true) && !x.isRemoved).Select(i => _mapper.Map<PageDto>(i)).ToList();
        }

        public PageDto AddPage(PageDto newPage)
        {
            if (newPage != null)
            {
                var page = _mapper.Map<Page>(newPage);
                var id = Guid.NewGuid();

                page.Id = id;

                _unitOfWork.PageRepository.Add(page);
                _unitOfWork.Save();

                var result = _unitOfWork.PageRepository.GetByID(id);
                return _mapper.Map<PageDto>(result);
            }
            return null;
        }

        public PageDto MarkAsRemovedPage(Guid id)
        {
            if (id != Guid.Empty)
            {
                var page = _unitOfWork.PageRepository.GetByID(id);
                if (page != null)
                {
                    page.isRemoved = true;
                    var updatedPage = UpdatePage(page);
                    return _mapper.Map<PageDto>(updatedPage);
                }
                return null;
            }
            return null;
        }

        public PageDto ChangePage(PageDto pageDto)
        {
            if (pageDto != null && pageDto.Id != null)
            {
                var page = _mapper.Map<Page>(pageDto);
                var result = UpdatePage(page);
                return _mapper.Map<PageDto>(result);
            }
            return null;
        }

        private Page UpdatePage(Page page)
        {
            _unitOfWork.PageRepository.Update(page);
            _unitOfWork.Save();
            return _unitOfWork.PageRepository.GetByID(page.Id);
        }

        public List<PageListItemDto> GetPages(PageFilter filter)
        {
            return _unitOfWork.PageRepository.Get(x =>
                (filter.isSection != null ? x.isSection == filter.isSection : true) &&
                (filter.isRemoved != null ? x.isRemoved == filter.isRemoved : true))
                    .Select(i => _mapper.Map<PageListItemDto>(i)).ToList();
        }

        public List<PageListItemDto> GetFreePages()
        {
            var linkedPageId = _unitOfWork.LinkRepository.Get().Select(item => item.TargetPageId);
            var result = _unitOfWork.PageRepository.Get()
                .Where(item => !item.isRemoved && linkedPageId.Any(linkedId => !(linkedId == item.Id)))
                .Select(i => _mapper.Map<PageListItemDto>(i)).ToList();
            return result;
        }
    }
}