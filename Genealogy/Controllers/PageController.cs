using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Helpers;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;
using System;


namespace Genealogy.Controllers
{
    [Produces("application/json")]
    [Route("api/page")]
    public class PageController : Controller
    {
        private IGenealogyService _genealogyService;
        public PageController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }
        /// <summary>
        /// Добавить страницу
        /// </summary>
        /// <param name="newPage"></param>-
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] PageDto newPage)
        {
            PageDto resultPage = null;
            try
            {
                resultPage = _genealogyService.AddPage(newPage);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultPage);
        }

        /// <summary>
        /// Получить страницу
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] PageFilter filter)
        {
            List<PageDto> result = null;
            try
            {
                result = _genealogyService.GetPage(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Пометить как удаленное
        /// </summary>
        /// <param name="changedPage"></param>
        /// <returns></returns>
        [HttpPost("remove")]
        public IActionResult Remove([FromBody] PageDto changedPage)
        {
            PageDto resultPage = null;
            if (changedPage != null && changedPage.Id != null && changedPage.Id != Guid.Empty)
            {
                try
                {
                    resultPage = _genealogyService.MarkAsRemovedPage(changedPage.Id.Value);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultPage);
            }
            return new NoContentResult();
        }

        /// <summary>
        /// Изменить страницу
        /// </summary>
        /// <param name="changedPage"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] PageDto changedPage)
        {
            PageDto resultPage = null;
            if (changedPage != null && changedPage.Id != null && changedPage.Id != Guid.Empty)
            {
                try
                {
                    resultPage = _genealogyService.ChangePage(changedPage);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultPage);
            }
            return new NoContentResult();
        }


        /// <summary>
        /// Получить страницу
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetPages([FromQuery] PageFilter filter)
        {
            List<PageListItemDto> result = null;
            try
            {
                result = _genealogyService.GetPages(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}