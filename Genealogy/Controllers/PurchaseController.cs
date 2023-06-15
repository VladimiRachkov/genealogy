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
            BusinessObjectOutDto result;
            try
            {
                result = _genealogyService.ActivatePurchase(filter.Id.Value).Result;
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

        [HttpPost("new")]
        [Authorize(Roles = "Администратор")]
        public IActionResult Create([FromBody] PurchaseDto purchaseDto) {
            bool result = false;

            if (purchaseDto == null || purchaseDto.UserId == null || purchaseDto.ProductId == null) {
                return BadRequest("Ошибка запроса на создание покупки для пользователя");
            }
            try {
                var userId = purchaseDto.UserId ?? Guid.Empty;
                var productId = purchaseDto.ProductId ?? Guid.Empty;
                result = _genealogyService.ProductAction(productId, userId).Result;
            }
            catch (AppException ex) {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}