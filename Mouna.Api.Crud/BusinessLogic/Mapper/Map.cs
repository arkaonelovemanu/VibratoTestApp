using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.DataAccess.Models;
using Mouna.Api.Crud.Entities;

namespace Mouna.Api.Crud.BusinessLogic.Mapper
{
    public static class Map
    {
        public static Employee ToEntity(EmployeeBLL inputModel)
        {
            return new Employee
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Salary = inputModel.Salary
            };
        }

        public static List<Employee> ToEntity(List<EmployeeBLL> inputModelList)
        {
            return inputModelList.ConvertAll(x => new Employee { Id = x.Id,Name=x.Name,Salary=x.Salary });
        }

        public static EmployeeDAL ToDataAccess(EmployeeBLL inputModel)
        {
            return new EmployeeDAL
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Salary = inputModel.Salary
            };
        }

        public static List<EmployeeBLL> ToBLL(List<EmployeeDAL> inputModelList)
        {
            return inputModelList.ConvertAll(x => new EmployeeBLL { Id = x.Id, Name = x.Name, Salary = x.Salary });
        }

        public static EmployeeBLL ToBLL(EmployeeDAL inputModel)
        {
            return new EmployeeBLL
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Salary = inputModel.Salary
            };
        }
    }
}
