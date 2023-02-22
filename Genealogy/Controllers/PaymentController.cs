using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using System.Threading.Tasks;
using Genealogy.Models;
using Genealogy.Service.Helpers;
using Yandex.Checkout.V3;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Genealogy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/catalog")]
    public class PaymentController : Controller
    {
        private IGenealogyService _genealogyService;
        public PaymentController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        [HttpPost("payment")]
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

        [HttpPost("purchase")]
        public IActionResult Confirm([FromBody] Payment payment)
        {
            BusinessObjectOutDto result = null;
            try
            {
                //Message message = Client.ParseMessage(Request.Method, Request.ContentType, Request.Body);
                //Payment payment = message?.Object;


                result = _genealogyService.ConfirmPurchaseByPayment(payment);

            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}