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

        private static EmployeeBLL ToDomainModel(Employee inputModel)
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
