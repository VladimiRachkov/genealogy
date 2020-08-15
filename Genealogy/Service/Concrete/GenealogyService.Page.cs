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

        public PageDto RemovePage(Guid id)
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
                var page = _unitOfWork.PageRepository.GetByID(pageDto.Id);
                page.isSection = pageDto.IsSection != null ? pageDto.IsSection.Value : page.isSection;
                page.Name = pageDto.Name != null ? pageDto.Name : page.Name;
                page.Title = pageDto.Title != null ? pageDto.Title : page.Title;
                page.Content = pageDto.Content != null ? pageDto.Content : page.Content;

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

        public PageWithLinksDto GetPageWithLinks(PageFilter filter)
        {
            var pages = _unitOfWork.PageRepository.Get();
            var page = pages.Where(x => (filter.Id != Guid.Empty ? x.Id == filter.Id : true) && (filter.Name != null ? x.Name == filter.Name : true) && !x.isRemoved).FirstOrDefault();

            LinkFilter linkFilter = new LinkFilter()
            {
                isRemoved = false,
                PageId = page.Id
            };

            var links = GetLinks(linkFilter).FindAll(link => link.TargetPageId != page.Id).Select(l =>
            {
                var linkedPage = pages.Where(p => p.Id == l.TargetPageId).FirstOrDefault();
                return new ShortLinkDto() { Caption = linkedPage.Title, Route = linkedPage.Name, Order = l.Order };
            });
            var result = _mapper.Map<PageWithLinksDto>(page);
            result.Links = links;
            return result;
        }
    }
}