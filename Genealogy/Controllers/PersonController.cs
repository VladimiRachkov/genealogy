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
    [Route("api/persons")]
    public class PersonController : Controller
    {
        private IGenealogyService _genealogyService;
        public PersonController(IGenealogyService genealogyService)
        {
            _genealogyService = genealogyService;
        }

        /// <summary>
        /// Добавить 
        /// </summary>
        /// <param name="newPerson"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] PersonDto newPerson)
        {
            PersonDto resultPerson = null;
            try
            {
                resultPerson = _genealogyService.AddPerson(newPerson);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(resultPerson);
        }

        /// <summary>
        /// Получить список 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] PersonFilter filter)
        {
            List<PersonDto> result = null;
            try
            {
                result = _genealogyService.GetPerson(filter);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Получить список 
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] PersonFilter filter)
        {
            List<PersonDto> result = null;
            try
            {
                result = _genealogyService.GetAllPersons(filter);
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
        /// <param name="changedPerson"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] PersonDto changedPerson)
        {
            PersonDto resultPerson = null;
            if (changedPerson != null && changedPerson.Id != null && changedPerson.Id != Guid.Empty)
            {
                try
                {
                    resultPerson = _genealogyService.ChangePerson(changedPerson);
                }
                catch (AppException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(resultPerson);
            }
            return new NoContentResult();
        }
        
        [AllowAnonymous]
        [HttpGet("count")]
        public IActionResult GetCount()
        {
            CountOutDto result;
            try
            {
                result = _genealogyService.GetPersonsCount();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }
    }
}