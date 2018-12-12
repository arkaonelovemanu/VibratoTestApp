using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.BusinessLogic.Mapper;
using Mouna.Api.Crud.BusinessLogic.Interfaces;
using Mouna.Api.Crud.DataAccess.Interfaces;
using Mouna.Api.Crud.Lib;
using Microsoft.Extensions.Logging;

namespace Mouna.Api.Crud.BusinessLogic.Services
{
    public class EmployeeService:IEmployeeService
    {

       // private readonly List<EmployeeBLL> employees;
        private  IMapBLL _mapper;
        private ILogger _logger;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapBLL mapper, IEmployeeRepository repo, ILogger<EmployeeService> logger)
        {
            this._mapper = mapper;
            this._employeeRepository = repo;
            this._logger = logger;
        }

        public ResponseData<List<EmployeeBLL>> GetEmployees()
        {
            _logger.LogInformation(LoggingEvents.GetAllEmployees, "BLL:Getting all employees");
            try
            {
                return _mapper.ToBLL(_employeeRepository.GetEmployees());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.GetAllEmployees, "BLL:Getting all employees  exception", ex.Message);
                return new ResponseData<List<EmployeeBLL>> { returnCode = APIErrorCode.InternalServerError };
            }
            
        }

        public ResponseData<List<EmployeeBLL>> GetEmployee(int id)
        {
            
            _logger.LogInformation(LoggingEvents.GetEmployeeById, "BLL:Getting employee by id {0}", id);
            try
            {
                return _mapper.ToBLL(_employeeRepository.GetEmployee(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.GetEmployeeById, "BLL:Getting employee by id exception", ex.Message);
                return new ResponseData<List<EmployeeBLL>> { returnCode = APIErrorCode.InternalServerError };
            }
        }

        public ResponseData<List<EmployeeBLL>> AddEmployee(EmployeeBLL item)
        { 
            _logger.LogInformation(LoggingEvents.AddEmployee, "BLL:Adding employee by id {0}", item.Id);
            try
            {
                return _mapper.ToBLL(_employeeRepository.AddEmployee(_mapper.ToDataAccess(item)));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.AddEmployee, "BLL:Adding employee by id exception", ex.Message);
                return new ResponseData<List<EmployeeBLL>> { returnCode = APIErrorCode.InternalServerError };
            }
        }

        public ResponseData<List<EmployeeBLL>> UpdateEmployee(EmployeeBLL item)
        {
            _logger.LogInformation(LoggingEvents.UpdateEmployee, "BLL:Updating employee by id {0}", item.Id);
            try
            {
                return _mapper.ToBLL(_employeeRepository.UpdateEmployee(_mapper.ToDataAccess(item)));
            }
            catch(Exception ex)
            {
                _logger.LogInformation(LoggingEvents.UpdateEmployee, "BLL:Updating employee by id exception", ex.Message);
                return new ResponseData<List<EmployeeBLL>> { returnCode = APIErrorCode.InternalServerError };
            }
       
        }

        public ResponseData<List<EmployeeBLL>> DeleteEmployee(int id)
        {

            _logger.LogInformation(LoggingEvents.DeleteEmployee, "BLL:Deleting employee by id {0}", id);
            try
            {
                return _mapper.ToBLL(_employeeRepository.DeleteEmployee(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.DeleteEmployee, "BLL:Deleting employee by id exception", ex.Message);
                return new ResponseData<List<EmployeeBLL>> { returnCode = APIErrorCode.InternalServerError };
            }
        }

        public bool EmployeeExists(int id)
        {
            _logger.LogInformation(LoggingEvents.UpdateEmployee, "BLL:Checking employee by id {0}", id);
            return _employeeRepository.GetEmployee(id).Data.ToList().Any(m => m.Id == id);
        }
    }
}
