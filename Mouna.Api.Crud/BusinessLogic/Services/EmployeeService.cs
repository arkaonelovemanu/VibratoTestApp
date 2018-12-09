using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.BusinessLogic.Mapper;
using Mouna.Api.Crud.BusinessLogic.Interfaces;

namespace Mouna.Api.Crud.BusinessLogic.Services
{
    public class EmployeeService:IEmployeeService
    {

        private readonly List<EmployeeBLL> employees;

        public EmployeeService()
        {
            this.employees = new List<EmployeeBLL>
            {
                new EmployeeBLL { Id = 1, Name="Doug Stark", Salary="10000" },
                new EmployeeBLL { Id = 2, Name="Jon Michael", Salary="20000" },
                new EmployeeBLL { Id = 3, Name="Kirk Hamett", Salary="30000" },
                new EmployeeBLL { Id = 4, Name="Robert Plant", Salary="30000" }
            };
        }

        public List<Employee> GetEmployees()
        {
            return Map.ToEntity(this.employees.ToList());

        }

        public Employee GetEmployee(int id)
        {
            return Map.ToEntity(this.employees.Where(m => m.Id == id).FirstOrDefault());
        }
    }
}
