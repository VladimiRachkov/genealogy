using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using Microsoft.AspNetCore.Authorization;
using Genealogy.Service.Helpers;
using Genealogy.Models;

namespace Genealogy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/subscription")]
    public class SubscriptionController : Controller
    {
        private IGenealogyService _genealogyService;
        public SubscriptionController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            BusinessObjectOutDto result = null;
            try
            {
                result = _genealogyService.GetActiveSubscription();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}