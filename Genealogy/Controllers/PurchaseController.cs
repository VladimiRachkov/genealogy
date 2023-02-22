using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using Microsoft.AspNetCore.Authorization;
using Genealogy.Models;
using System.Threading.Tasks;
using Genealogy.Service.Helpers;
using System;

namespace Genealogy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/purchase")]
    public class PurchaseController : Controller
    {
        private IGenealogyService _genealogyService;
        public PurchaseController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Активировать продукт
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public IActionResult Send([FromBody] BusinessObjectFilter filter)
        {
            Task result;
            try
            {
                result = _genealogyService.ActivatePurchase(filter.Id.Value);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Администратор")]
        public IActionResult RemovePurchase(Guid id)
        {
            BusinessObjectOutDto result = null;

            try
            {
                result = _genealogyService.RemoveBusinessObject(id);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}