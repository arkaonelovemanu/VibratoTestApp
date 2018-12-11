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

namespace Mouna.Api.Crud.BusinessLogic.Services
{
    public class EmployeeService:IEmployeeService
    {

       // private readonly List<EmployeeBLL> employees;
        private  IMapBLL _mapper;

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IMapBLL mapper, IEmployeeRepository repo)
        {
            this._mapper = mapper;
            this._employeeRepository = repo;
        }

        public ResponseData<List<EmployeeBLL>> GetEmployees()
        {
            return _mapper.ToBLL(_employeeRepository.GetEmployees());
        }

        public ResponseData<List<EmployeeBLL>> GetEmployee(int id)
        {
            return _mapper.ToBLL(_employeeRepository.GetEmployees());
        }

        public ResponseData<List<EmployeeBLL>> AddEmployee(EmployeeBLL item)
        {
            return _mapper.ToBLL(_employeeRepository.AddEmployee(_mapper.ToDataAccess(item)));
        }

        public ResponseData<List<EmployeeBLL>> UpdateEmployee(EmployeeBLL item)
        {
            return _mapper.ToBLL(_employeeRepository.UpdateEmployee(_mapper.ToDataAccess(item)));
        }

        public ResponseData<List<EmployeeBLL>> DeleteEmployee(int id)
        {
            return _mapper.ToBLL(_employeeRepository.DeleteEmployee(id));
        }

        public bool EmployeeExists(int id)
        {
            return _employeeRepository.GetEmployee(id).Data.ToList().Any(m => m.Id == id);
        }
    }
}
