using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.Controllers.Mapper
{
    public interface IMap
    {
        EmployeeBLL ToDomainModel(Employee inputModel);
        //Employee ToEntity(EmployeeBLL inputModel);
        ResponseData<List<Employee>> ToEntity(ResponseData<List<EmployeeBLL>> responseFromBLL);

    }
}
