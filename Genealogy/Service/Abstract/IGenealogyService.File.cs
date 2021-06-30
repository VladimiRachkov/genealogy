using System.Threading.Tasks;
using Genealogy.Models;
using Microsoft.AspNetCore.Http;

namespace Genealogy.Service.Astract
{
    partial interface IGenealogyService
    {
        Response AddFile(IFormFile uploadedFile);
    }
}