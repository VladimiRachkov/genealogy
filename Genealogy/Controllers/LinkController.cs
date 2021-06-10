using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Helpers;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Genealogy.Controllers
{
    [Authorize]
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
        /// Получить ссылки для страницы
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
            List<LinkDto> result = null;
            try
            {
                result = _genealogyService.AddLink(newLink);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpPost("list")]
        public IActionResult UpdateLinks([FromBody] LinkListDto linkList)
        {
            if (linkList.links == null)
            {
                return BadRequest("Пустой список");
            }
            List<LinkDto> result = null;
            try
            {
                result = _genealogyService.UpdateLinks(linkList.links);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);

        }

    }
}