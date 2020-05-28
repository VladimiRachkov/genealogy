using Microsoft.AspNetCore.Mvc;
using Genealogy.Models.Domain.Dtos;
using Genealogy.Service.Helpers;
using Genealogy.Service.Astract;
using Genealogy.Models;

namespace Genealogy.Controllers
{
    [Produces("application/json")]
    [Route("api/cemetery")]
    public class CemeteryController : Controller
    {
        private IGenealogyService _genealogyService;
        public CemeteryController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        [HttpPost]
        public IActionResult AddCemetery([FromBody]CemeteryDto newCemetery)
        {
            Cemetery resultCemetery = null;
            try
            {
                resultCemetery = _genealogyService.AddCemetery(newCemetery);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultCemetery);
        }
    }
}