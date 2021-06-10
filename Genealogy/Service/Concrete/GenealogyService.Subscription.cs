using System.Threading.Tasks;
using Genealogy.Models;
using Genealogy.Service.Astract;
using System.Linq;
using Genealogy.Data;
using System;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public BusinessObjectOutDto GetActiveSubscription()
        {
            BusinessObjectOutDto result = null;

            var userId = GetCurrentUserId();
            var filter = new BusinessObjectFilter()
            {
                MetatypeId = MetatypeData.Subscribe.Id
            };

            var bos = GetBusinessObjects(filter);
            var subscribes = bos.Where(bo => bo.FinishDate > DateTime.Now && bo.UserId == userId);

            if (subscribes.Any())
            {
                var subscribe = subscribes.FirstOrDefault();
                result = GetBusinessObjectsDto(new BusinessObjectFilter() { Id = subscribe.Id }).FirstOrDefault();
            }

            return result;
        }
    }
}