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
        private readonly IEmployeeService _employeeService;
        private  ILogger _logger;
        private  IMap _mapper;
        private ResponseData<List<Employee>> _responseData = new ResponseData<List<Employee>>();

        public EmployeeController(IEmployeeService service, IMap mapper, ILogger<EmployeeController> log)
        {
            _mapper = mapper;
            _employeeService = service;
            _logger = log;
        }
        // GET: api/v1/employees
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation(LoggingEvents.GetAllEmployees, "Getting employee list");

                _responseData = _mapper.ToEntity(_employeeService.GetEmployees());

            if (_responseData.returnCode == APIErrorCode.Ok)
                return Ok(_responseData.Data);
            else
                throw new ApplicationException("An exception occurred in one of the layers") ;
        }

        // GET api/v1/employees/5
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetEmployeeById, "Getting employee {ID}", id);
            _responseData = _mapper.ToEntity(_employeeService.GetEmployee(id));
            if (_responseData.Data == null)
            {
                _logger.LogWarning(LoggingEvents.GetEmployeeNotFound, "Employee ({ID}) NOT FOUND", id);
                return NotFound();
            }

            if (_responseData.returnCode == APIErrorCode.Ok)
                return Ok(_responseData.Data);
            else
                throw new ApplicationException("An exception occurred in one of the layers");
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult Post([FromBody]Employee inputModel)
        {
            if (inputModel == null)
            {
                _logger.LogWarning(LoggingEvents.InputModelFormatIncorrect, "Incorrect format of input model");
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(LoggingEvents.InputModelFormatIncorrect, "Model state is not valid ");
                return Unprocessable(ModelState);
            }
            _logger.LogInformation(LoggingEvents.AddEmployee, "Trying to add employee {Name}", inputModel.Name);
            _employeeService.AddEmployee(_mapper.ToDomainModel(inputModel));
            _logger.LogInformation(LoggingEvents.AddEmployee, "Adding employee {Name} successful", inputModel.Name);

            if (_responseData.returnCode == APIErrorCode.Created)
                return CreatedAtRoute("GetEmployeeById", new { id = _responseData.Data.First().Id }, _responseData.Data.First());
            else
                throw new ApplicationException("An exception occurred in one of the layers");
            
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
            {
                _logger.LogWarning(LoggingEvents.InputModelFormatIncorrect, "Incorrect format of input model");
                return BadRequest();
            }

            if (!_employeeService.EmployeeExists(id))
            {
                _logger.LogWarning(LoggingEvents.GetEmployeeNotFound,"Employee with ID { 0} does not exist ", id);
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(LoggingEvents.InputModelFormatIncorrect, "Model state is not valid ");
                return new UnprocessableObjectResult(ModelState);
            }

            _logger.LogInformation(LoggingEvents.AddEmployee, "Trying to update employee {Name}", inputModel.Name);
            _responseData=_mapper.ToEntity(_employeeService.UpdateEmployee(_mapper.ToDomainModel(inputModel)));
            _logger.LogInformation(LoggingEvents.AddEmployee, "Updating employee {Name} successful", inputModel.Name);
            if (_responseData.returnCode == APIErrorCode.NoContent)
                return NoContent();
            else
                throw new ApplicationException("An exception occurred in one of the layers");
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ResponseData<List<Employee>> _responseData = new ResponseData<List<Employee>>();
            if (!_employeeService.EmployeeExists(id))
            {
                _logger.LogWarning(LoggingEvents.GetEmployeeNotFound, "Employee with ID { 0} does not exist ", id);
                return NotFound();
            }
            _logger.LogInformation(LoggingEvents.AddEmployee, "Trying to delete employee {0}", id);
            _responseData = _mapper.ToEntity(_employeeService.DeleteEmployee(id));
            _logger.LogInformation(LoggingEvents.AddEmployee, "Deleted employee {0}", id);
            if (_responseData.returnCode == APIErrorCode.NoContent)
                return NoContent();
            else
                throw new ApplicationException("An exception occurred in one of the layers");
        }
    }
}
