using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        ResponseData<List<EmployeeBLL>> GetEmployees();
        ResponseData<List<EmployeeBLL>> GetEmployee(int id);
        ResponseData<List<EmployeeBLL>> AddEmployee(EmployeeBLL item);
        ResponseData<List<EmployeeBLL>> UpdateEmployee(EmployeeBLL item);
        ResponseData<List<EmployeeBLL>> DeleteEmployee(int id);
        bool EmployeeExists(int id);
    }
}
