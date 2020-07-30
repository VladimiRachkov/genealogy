using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Helpers;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;

namespace Genealogy.Controllers
{
    [Produces("application/json")]
    [Route("api/link")]
    public class LinkController : Controller
    {
        private IGenealogyService _genealogyService;
        public LinkController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Получить ссылка для страницы
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPages([FromQuery] LinkFilter filter)
        {
            List<LinkDto> result = null;
            try
            {
                result = _genealogyService.GetLinks(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Добавление новой ссылки на страницу
        /// </summary>
        /// <param name="newLink">Ссылка</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] LinkDto newLink)
        {
            LinkDto resultPage = null;
            try
            {
                resultPage = _genealogyService.AddLink(newLink);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultPage);
        }

    }
}