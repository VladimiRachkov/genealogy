using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;
using Genealogy.Service.Helpers;
using System.Threading.Tasks;

namespace Genealogy.Controllers
{
    [Produces("application/json")]
    [Route("api/mail")]
    public class MailController : Controller
    {
        private IGenealogyService _genealogyService;
        public MailController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Отправить письмо
        /// </summary>
        /// <returns></returns>
        // [HttpPost]
        // public IActionResult Send([FromQuery] MetatypeFilter filter)
        // {
        //     Task result;
        //     try
        //     {
        //         result = _genealogyService.SendEmailAsync("vladimir.amelkin87@yandex.ru", "Test", "Test");
        //     }
        //     catch (AppException ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        //     return Ok(result);
        // }
    }
}