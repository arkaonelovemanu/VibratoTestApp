using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mouna.Api.Crud.Lib;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.BusinessLogic.Interfaces;
using Mouna.Api.Crud.Controllers.Mapper;
using Microsoft.Extensions.Logging;

namespace Mouna.Api.Crud.Controllers.V1
{
    [Route("api/V1/[controller]")]
    public class EmployeeController :BaseController
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger logger;

        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> log)
        {
            employeeService = service;
            logger = log;
        }
        // GET: api/v1/employees
        [HttpGet]
        public IActionResult Get()
        {
            logger.LogInformation(LoggingEvents.GetAllEmployees, "Getting employee list");
            List<Employee> emp = Map.ToEntity(employeeService.GetEmployees());

            //var outputModel = ToOutputModel(model);
            return Ok(emp);
        }

        // GET api/v1/employees/5
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public IActionResult Get(int id)
        {
            logger.LogInformation(LoggingEvents.GetEmployeeById, "Getting employee {ID}", id);
            Employee emp = Map.ToEntity(employeeService.GetEmployee(id));
            if (emp == null)
            {
                logger.LogWarning(LoggingEvents.GetEmployeeNotFound, "Employee ({ID}) NOT FOUND", id);
                return NotFound();
            }

            // var outputModel = ToOutputModel(model);
            return Ok(emp);
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult Post([FromBody]Employee inputModel)
        {
            if (inputModel == null)
            {
                logger.LogWarning(LoggingEvents.InputModelFormatIncorrect, "Incorrect format of input model");
                return BadRequest();
            }

            if (!ModelState.IsValid)
                return Unprocessable(ModelState);
            var outputModel = Map.ToDomainModel(inputModel);
            logger.LogInformation(LoggingEvents.AddEmployee, "Trying to add employee {Name}", inputModel.Name);
            employeeService.AddEmployee(outputModel);
            logger.LogInformation(LoggingEvents.AddEmployee, "Adding employee {Name} successful", inputModel.Name);
            return CreatedAtRoute("GetEmployeeById", new { id = outputModel.Id }, outputModel);
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            if (!employeeService.EmployeeExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return new UnprocessableObjectResult(ModelState);

            employeeService.UpdateEmployee(Map.ToDomainModel(inputModel));

            return NoContent();
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!employeeService.EmployeeExists(id))
                return NotFound();

            employeeService.DeleteEmployee(id);

            return NoContent();
        }
    }
}
