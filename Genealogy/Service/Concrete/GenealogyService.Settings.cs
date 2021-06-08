using System.Linq;
using Genealogy.Models;
using Genealogy.Service.Astract;

namespace Genealogy.Service.Concrete
{
    public partial class GenealogyService : IGenealogyService
    {
        public string GetSettingValue(string name)
        {
            var filter = new BusinessObjectFilter()
            {
                Name = name
            };

            return GetBusinessObjectsDto(filter).FirstOrDefault().Data;
        }

    }
}