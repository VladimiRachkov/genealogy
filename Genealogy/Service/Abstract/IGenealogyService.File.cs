using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        long AddFile(IFormFile uploadedFile);
    }
}