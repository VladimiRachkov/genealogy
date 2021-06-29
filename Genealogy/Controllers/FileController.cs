using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Genealogy.Service.Helpers;
using Genealogy.Models;

namespace Genealogy.Controllers
{
    [Authorize]
    [Route("api/file")]
    public class FileController : Controller
    {
        private IGenealogyService _genealogyService;
        public FileController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }
        [HttpPost]
        public async Task<IActionResult> AddFile([FromForm] UploadModel uploadedFile)
        {
            long result = 0;

            try
            {
                result = _genealogyService.AddFile(uploadedFile.File);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}