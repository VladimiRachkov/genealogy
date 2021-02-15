using Microsoft.AspNetCore.Mvc;
using Genealogy.Service.Astract;
using System.Threading.Tasks;
using Genealogy.Models;
using System.Collections.Generic;
using Genealogy.Service.Helpers;

namespace Genealogy.Controllers
{
    [Produces("application/json")]
    [Route("api/payment")]
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
                resultPage = _genealogyService.CreateBusinessObject(businessObject);
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
        [HttpPost]
        public IActionResult Update([FromBody] BusinessObjectInDto businessObject)
        {
            BusinessObjectOutDto resultPage = null;
            try
            {
                resultPage = _genealogyService.CreateBusinessObject(businessObject);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultPage);
        }
    }
}