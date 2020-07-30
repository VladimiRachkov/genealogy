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
        public List<LinkDto> GetLinks(LinkFilter filter)
        {
            return _unitOfWork.LinkRepository.Get(x =>
                (filter.isRemoved != null ? x.isRemoved == filter.isRemoved : true) &&
                (filter.PageId != Guid.Empty ? x.PageId == filter.PageId : true))
                    .Select(i => _mapper.Map<LinkDto>(i)).ToList();
        }
        public LinkDto AddLink(LinkDto link)
        {
            LinkFilter linkFilter = new LinkFilter()
            {
                isRemoved = false,
                PageId = link.PageId
            };

            var links = this.GetLinks(linkFilter);
            int maxOrder = 0;
            if (links.Any())
            {
                maxOrder = links.Select(item => item.Order).Max();
            }

            var newLink = _mapper.Map<Link>(link);

            newLink.Id = Guid.NewGuid();
            newLink.isRemoved = false;
            newLink.Order = maxOrder += 1;

            _unitOfWork.LinkRepository.Add(newLink);
            _unitOfWork.Save();

            var result = _unitOfWork.LinkRepository.GetByID(newLink.Id);
            return _mapper.Map<LinkDto>(result);
        }
    }
}