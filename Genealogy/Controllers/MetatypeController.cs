using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;
using Genealogy.Service.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Genealogy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/metatype")]
    public class MetatypeController : Controller
    {
        private IGenealogyService _genealogyService;
        public MetatypeController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Получить список бизнес-объектов 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] MetatypeFilter filter)
        {
            List<MetatypeOutDto> result = null;
            try
            {
                result = _genealogyService.GetMetatypes(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}