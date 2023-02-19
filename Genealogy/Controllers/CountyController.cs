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
    [Route("api/county")]
    public class CountyController : Controller
    {
        private IGenealogyService _genealogyService;
        public CountyController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Добавить кладбище
        /// </summary>
        /// <param name="newCounty"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] CountyDto newCounty)
        {
            CountyDto resultCounty = null;
            try
            {
                resultCounty = _genealogyService.AddCounty(newCounty);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultCounty);
        }

        /// <summary>
        /// Получить список уездов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] CountyFilter filter)
        {
            List<CountyDto> result = null;
            try
            {
                result = _genealogyService.GetCounty(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Удаление уезда по Id
        /// </summary>
        /// <param name="countyId"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            CountyDto resultCounty = null;
            if (id != null && id != Guid.Empty)
            {
                try
                {
                    resultCounty = _genealogyService.RemoveCounty(id);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultCounty);
            }
            return new NoContentResult();
        }

        /// <summary>
        /// Изменить поля 
        /// </summary>
        /// <param name="countyId"></param>
        /// <returns></returns>
        [HttpPost("{id}/restore")]
        public IActionResult Restore(Guid id)
        {
            CountyDto resultCounty = null;
            if (id != null && id != Guid.Empty)
            {
                try
                {
                    resultCounty = _genealogyService.RestoreCounty(id);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultCounty);
            }
            return new NoContentResult();
        }

        [HttpPut]
        public IActionResult Put([FromBody] CountyDto changedCounty)
        {
            CountyDto resultCounty = null;
            if (changedCounty != null && changedCounty.Id != null && changedCounty.Id != Guid.Empty)
            {
                try
                {
                    resultCounty = _genealogyService.ChangeCounty(changedCounty);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultCounty);
            }
            return new NoContentResult();
        }
    }
}