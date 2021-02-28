using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using Genealogy.Models;
using System.Collections.Generic;
using Genealogy.Service.Helpers;

namespace Genealogy.Controllers
{
    [Produces("application/json")]
    [Route("api/businessobject")]
    public class BusinessObjectController : Controller
    {
        private IGenealogyService _genealogyService;
        public BusinessObjectController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Получить список бизнес-объектов 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] BusinessObjectFilter filter)
        {
            List<BusinessObjectOutDto> result = null;
            
            try
            {
                result = _genealogyService.GetBusinessObjects(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Добавить бизнес-объект
        /// </summary>
        /// <param name="businessObject"></param>-
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] BusinessObjectInDto businessObject)
        {
            BusinessObjectOutDto resultPage = null;

            try
            {
                resultPage = _genealogyService.CreateBusinessObjectsFromDto(businessObject);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultPage);
        }

        /// <summary>
        /// Изменить бизнес-объект
        /// </summary>
        /// <param name="businessObject"></param>-
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] BusinessObjectInDto businessObject)
        {
            BusinessObjectOutDto resultPage = null;

            try
            {
                resultPage = _genealogyService.UpdateBusinessObject(businessObject);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultPage);
        }

        /// <summary>
        /// Получить кол-во объектов
        /// </summary>
        /// <returns></returns>
        [HttpGet("count")]
        public IActionResult GetCount([FromQuery] BusinessObjectFilter filter)
        {
            BusinessObjectsCountOutDto result = null;

            try
            {
                result = _genealogyService.GetBusinessObjectsCount(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}