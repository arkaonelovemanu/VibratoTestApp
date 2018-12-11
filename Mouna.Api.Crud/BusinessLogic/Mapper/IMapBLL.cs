using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.DataAccess.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.BusinessLogic.Mapper
{
    public interface IMapBLL
    {
        EmployeeDAL ToDataAccess(EmployeeBLL inputModel);
        ResponseData<List<EmployeeBLL>> ToBLL(ResponseData<IEnumerable<EmployeeDAL>> inputModelList);
       // EmployeeBLL ToBLL(EmployeeDAL inputModel);


    }
}
