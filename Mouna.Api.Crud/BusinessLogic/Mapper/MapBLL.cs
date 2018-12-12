using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mouna.Api.Crud.BusinessLogic.Models;
using Mouna.Api.Crud.DataAccess.Models;
using Mouna.Api.Crud.Entities;
using Mouna.Api.Crud.Lib;

namespace Mouna.Api.Crud.BusinessLogic.Mapper
{
    public class MapBLL:IMapBLL
    {

        public  EmployeeDAL ToDataAccess(EmployeeBLL inputModel)
        {
            return new EmployeeDAL
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Salary = inputModel.Salary
            };
        }

        public  ResponseData<List<EmployeeBLL>> ToBLL(ResponseData<IEnumerable<EmployeeDAL>> responseDataFromDAL)
        {
            return  new ResponseData<List<EmployeeBLL>> { Data = responseDataFromDAL.Data.ToList().ConvertAll(x => new EmployeeBLL { Id = x.Id, Name = x.Name, Salary = x.Salary }), returnCode = responseDataFromDAL.returnCode }; ;
        }
    }
}
