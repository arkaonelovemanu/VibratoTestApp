using Mouna.Api.Crud.DataAccess.Models;
using Mouna.Api.Crud.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.DataAccess.Interfaces
{
    public interface IEmployeeRepository
    {
        ResponseData<IEnumerable<EmployeeDAL>> GetEmployees();
        ResponseData<IEnumerable<EmployeeDAL>> GetEmployee(int id);
        ResponseData<IEnumerable<EmployeeDAL>> DeleteEmployee(int id);
        ResponseData<IEnumerable<EmployeeDAL>> AddEmployee(EmployeeDAL employee);
        ResponseData<IEnumerable<EmployeeDAL>> UpdateEmployee(EmployeeDAL employee);
    }
}
