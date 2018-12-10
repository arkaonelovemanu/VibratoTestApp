using Mouna.Api.Crud.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.DataAccess.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<EmployeeDAL> GetEmployees();
        IEnumerable<EmployeeDAL> GetEmployee(int id);
        int DeleteEmployee(int id);
        int AddEmployee(EmployeeDAL employee);
        int UpdateEmployee(EmployeeDAL employee);
    }
}
