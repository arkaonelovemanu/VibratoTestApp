using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mouna.Api.Crud.Lib
{
    public class LoggingEvents
    {
        public const int GetAllEmployees= 1000;
        public const int GetEmployeeById = 1001;
        public const int AddEmployee = 1002;
        public const int UpdateEmployee = 1003;
        public const int DeleteEmployee = 1004;
        public const int DeleteItem = 1005;

        public const int GetEmployeeNotFound = 4000;
        public const int UpdateEmployeeNotFound = 4001;
        public const int InputModelFormatIncorrect = 4002;
    }
}
