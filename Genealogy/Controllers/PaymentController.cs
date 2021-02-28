using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using System.Threading.Tasks;
using Genealogy.Models;
using Genealogy.Service.Helpers;
using System;

namespace Genealogy.Controllers
{
    [Produces("application/json")]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private IGenealogyService _genealogyService;
        public PaymentController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        [HttpPost]
        public async Task<IActionResult> DoPayment([FromBody] PaymentInDto payment)
        {
            string result = null;
            try
            {
                result = await _genealogyService.DoPayment(payment);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult ConfirmPayment([FromQuery] Guid purchaseId)
        {
            BusinessObjectOutDto result = null;
            var dto = new PurchaseInDto();
            dto.Id = purchaseId;

            try
            {
                result = _genealogyService.ConfirmPurchase(dto);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}