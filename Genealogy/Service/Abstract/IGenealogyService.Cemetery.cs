using Genealogy.Models;
using Genealogy.Models.Domain.Dtos;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Cemetery AddCemetery(CemeteryDto newCemetery);
    }
}