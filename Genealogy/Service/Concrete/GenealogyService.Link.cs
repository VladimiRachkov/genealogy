using System;
using System.Collections.Generic;
using System.Linq;
using Genealogy.Models;
using Genealogy.Service.Astract;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public List<LinkDto> GetLinks(LinkFilter filter)
        {
            return _unitOfWork.LinkRepository.Get(x =>
                ((filter.PageId != null && filter.PageId != Guid.Empty) ? x.PageId == filter.PageId : true))
                    .OrderBy(link => link.Order)
                    .Select(i => _mapper.Map<LinkDto>(i)).ToList();
        }
        public List<LinkDto> AddLink(LinkDto link)
        {
            LinkFilter linkFilter = new LinkFilter()
            {
                isRemoved = false,
                PageId = link.PageId
            };
            var links = this.GetLinks(linkFilter);

            if (links.Where(l => l.PageId == link.PageId && l.TargetPageId == link.TargetPageId).FirstOrDefault() != null)
            {
                var dublicate = GetLinkByIds(link.PageId, link.TargetPageId);
                _unitOfWork.LinkRepository.Delete(dublicate);
                _unitOfWork.Save();

                IndexingOrder();
                return GetLinks(new LinkFilter());
            }

            int maxOrder = 0;
            if (links.Any())
            {
                maxOrder = links.Select(item => item.Order).Max();
            }

            var newLink = _mapper.Map<Link>(link);

            newLink.Id = Guid.NewGuid();
            newLink.Order = maxOrder += 1;

            _unitOfWork.LinkRepository.Add(newLink);
            _unitOfWork.Save();

            return GetLinks(new LinkFilter());
        }

        public List<LinkDto> UpdateLinks(IEnumerable<LinkDto> links)
        {
            LinkFilter linkFilter = new LinkFilter()
            {
                isRemoved = false,
                PageId = links.FirstOrDefault().PageId
            };

            links.ToList().ForEach(link =>
            {
                var updatedLink = _mapper.Map<LinkDto, Link>(link);
                _unitOfWork.LinkRepository.Update(updatedLink);
            });

            _unitOfWork.Save();

            var result = GetLinks(linkFilter);
            return result;
        }

        private void IndexingOrder()
        {
            var links = _unitOfWork.LinkRepository.Get().OrderBy(link => link.Order).ToList();

            int i = 0;
            foreach (var link in links)
            {
                link.Order = i;
                _unitOfWork.LinkRepository.Update(link);
                i++;
            }
            _unitOfWork.Save();
        }

        private Link GetLinkByIds(Guid pageId, Guid targetPageId)
        {
            return _unitOfWork.LinkRepository.Get(x =>
                pageId == x.PageId && targetPageId == x.TargetPageId)
                .FirstOrDefault();
        }
    }
}