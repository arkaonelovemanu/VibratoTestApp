using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.Lib
{
    public class ResponseData<T>
    {
        public APIErrorCode returnCode { get; set; }
        public T Data { get; set; }

        public ResponseData(T _data)
        {
            Data = _data;
        }
        public ResponseData(T _data, APIErrorCode _code)
        {
            Data = _data;
            returnCode = _code;
        }

        public ResponseData()
        {

        }
    }

    public enum APIErrorCode
    {
        BadRequest=400,
        Unauthorized=403,
        EmployeeNotFound=404,
        InternalServerError=500,
        Ok=200,
        Created=201,
        NoContent =204,
        DatabaseConnectionIssue=600

    }
}
