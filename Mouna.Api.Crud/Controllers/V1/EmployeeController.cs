using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mouna.Api.Crud.Lib;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.BusinessLogic.Interfaces;

namespace Mouna.Api.Crud.Controllers.V1
{
    [Route("api/V1/[controller]")]
    public class EmployeeController :BaseController
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService service)
        {
            this.employeeService = service;
        }
        // GET: api/v1/employees
        [HttpGet]
        public IActionResult Get()
        {
            var model = employeeService.GetEmployees();

            //var outputModel = ToOutputModel(model);
            return Ok(model);
        }

        // GET api/v1/employees/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Employee emp = employeeService.GetEmployee(id);
            if (emp == null)
                return NotFound();

            // var outputModel = ToOutputModel(model);
            return Ok(emp);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        #region " Mappers "

        

      

        #endregion
    }
}
