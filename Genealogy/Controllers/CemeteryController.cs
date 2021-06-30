using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Helpers;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Genealogy.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/cemetery")]
    public class CemeteryController : Controller
    {
        private IGenealogyService _genealogyService;
        public CemeteryController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Добавить кладбище
        /// </summary>
        /// <param name="newCemetery"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] CemeteryDto newCemetery)
        {
            CemeteryDto resultCemetery = null;
            try
            {
                resultCemetery = _genealogyService.AddCemetery(newCemetery);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultCemetery);
        }

        /// <summary>
        /// Получить список кладбищ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] CemeteryFilter filter)
        {
            List<CemeteryDto> result = null;
            try
            {
                result = _genealogyService.GetCemetery(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Удаление кладбища по Id
        /// </summary>
        /// <param name="cemeteryId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            CemeteryDto resultCemetery = null;
            if (id != null && id != Guid.Empty)
            {
                try
                {
                    resultCemetery = _genealogyService.RemoveCemetery(id);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultCemetery);
            }
            return new NoContentResult();
        }

        /// <summary>
        /// Изменить поля 
        /// </summary>
        /// <param name="cemeteryId"></param>
        /// <returns></returns>
        [HttpPost("{id}/restore")]
        public IActionResult Restore(Guid id)
        {
            CemeteryDto resultCemetery = null;
            if (id != null && id != Guid.Empty)
            {
                try
                {
                    resultCemetery = _genealogyService.RestoreCemetery(id);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultCemetery);
            }
            return new NoContentResult();
        }

        [HttpPut]
        public IActionResult Put([FromBody] CemeteryDto changedCemetery)
        {
            CemeteryDto resultCemetery = null;
            if (changedCemetery != null && changedCemetery.Id != null && changedCemetery.Id != Guid.Empty)
            {
                try
                {
                    resultCemetery = _genealogyService.ChangeCemetery(changedCemetery);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultCemetery);
            }
            return new NoContentResult();
        }
    }
}