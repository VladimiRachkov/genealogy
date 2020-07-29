using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Helpers;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;
using System;

namespace Genealogy.Controllers
{
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
        /// Изменить поля 
        /// </summary>
        /// <param name="changedCemetery"></param>
        /// <returns></returns>
        [HttpPost("remove")]
        public IActionResult MarkAsRemoved([FromBody] CemeteryDto changedCemetery)
        {
            CemeteryDto resultCemetery = null;
            if (changedCemetery != null && changedCemetery.Id != null && changedCemetery.Id != Guid.Empty)
            {
                try
                {
                    resultCemetery = _genealogyService.MarkAsRemovedCemetery(changedCemetery.Id.Value);
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