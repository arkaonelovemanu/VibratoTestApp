using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.BusinessLogic.Mapper;
using Mouna.Api.Crud.BusinessLogic.Interfaces;
using Mouna.Api.Crud.DataAccess.Interfaces;

namespace Mouna.Api.Crud.BusinessLogic.Services
{
    public class EmployeeService:IEmployeeService
    {

        //private readonly List<EmployeeBLL> employees;

        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository repo)
        {
            this.employeeRepository = repo;
        }

        public List<EmployeeBLL> GetEmployees()
        {
            return Map.ToBLL(employeeRepository.GetEmployees().ToList());
        }

        public EmployeeBLL GetEmployee(int id)
        {
            return Map.ToBLL(employeeRepository.GetEmployee(id).ToList().Where(m => m.Id == id).FirstOrDefault());
        }

        public void AddEmployee(EmployeeBLL item)
        {
            int numberOfRowsAffected = employeeRepository.AddEmployee(Map.ToDataAccess(item));
        }

        public void UpdateEmployee(EmployeeBLL item)
        {
            int numberOfRowsAffected = employeeRepository.UpdateEmployee(Map.ToDataAccess(item));
        }

        public void DeleteEmployee(int id)
        {
            int numberOfRowsAffected = employeeRepository.DeleteEmployee(id);
        }

        public bool EmployeeExists(int id)
        {
            return employeeRepository.GetEmployee(id).ToList().Any(m => m.Id == id);
        }
    }
}
