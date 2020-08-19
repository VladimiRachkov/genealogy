using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using System.Threading.Tasks;
using Genealogy.Models;

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

        [HttpGet]
        public async Task<IActionResult> DoPayment([FromQuery] string returnUrl)
        {
            var result = await _genealogyService.DoPayment(returnUrl);
            return Ok(result);
        }
    }
}