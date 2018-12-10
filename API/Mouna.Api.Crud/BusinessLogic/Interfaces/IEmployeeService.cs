using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeBLL> GetEmployees();
        EmployeeBLL GetEmployee(int id);
        void AddEmployee(EmployeeBLL item);
        void UpdateEmployee(EmployeeBLL item);
        void DeleteEmployee(int id);
        bool EmployeeExists(int id);
    }
}
