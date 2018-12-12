using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.Lib;

namespace Mouna.Api.Crud.Controllers.Mapper
{
    public  class Map:IMap
    {
        public EmployeeBLL ToDomainModel(Employee inputModel)
        {
            return new EmployeeBLL
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Salary = inputModel.Salary
            };
        }
        public  ResponseData<List<Employee>> ToEntity(ResponseData<List<EmployeeBLL>> responseFromBLL)
        {
            return new ResponseData<List<Employee>> { Data= responseFromBLL.Data.ConvertAll(x => new Employee { Id = x.Id, Name = x.Name, Salary = x.Salary }), returnCode=responseFromBLL.returnCode } ;
        }
    }
}
