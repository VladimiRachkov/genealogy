using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using Microsoft.AspNetCore.Authorization;
using Genealogy.Service.Helpers;
using Genealogy.Models;
using System;

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
        public IActionResult Get([FromQuery] Guid? id)
        {
            BusinessObjectOutDto result = null;
            try
            {
                result = _genealogyService.GetActiveSubscription(id);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}