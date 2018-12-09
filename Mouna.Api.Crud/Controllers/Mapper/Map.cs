using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;

namespace Mouna.Api.Crud.Controllers.Mapper
{
    public static class Map
    {

        public static EmployeeBLL ToDomainModel(Employee inputModel)
        {
            return new EmployeeBLL
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Salary = inputModel.Salary
            };
        }

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
            return inputModelList.ConvertAll(x => new Employee { Id = x.Id, Name = x.Name, Salary = x.Salary });
        }
    }
}
