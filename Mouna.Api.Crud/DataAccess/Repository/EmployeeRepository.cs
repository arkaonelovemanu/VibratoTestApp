using Mouna.Api.Crud.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Mouna.Api.Crud.DataAccess.Interfaces;
using Mouna.Api.Crud.Lib;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Mouna.Api.Crud.DataAccess.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        //private IDbConnection _connection;
        private readonly IConfiguration _config;
        private  ILogger _logger;

        public EmployeeRepository(IConfiguration config, ILogger<EmployeeRepository> logger )
        {
            _config = config;
            _logger = logger;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        const string getAllEmployeesSqlQuery = "SELECT * FROM employeeForCRUD";
        const string getEmployeeByIdSqlQuery = "SELECT * FROM employeeForCRUD where Id= @Id";
        const string deleteEmployeeByIdSqlQuery = "DELETE FROM employeeForCRUD WHERE Id = @Id";
        const string insertEmployeeSqlQuery = "INSERT INTO employeeForCRUD Values (@Id,@Name,@Salary);";
        const string updateEmployeeSqlQuery = "UPDATE employeeForCRUD SET Name = @Name, Salary=@Salary WHERE Id = @Id;";
    
        public ResponseData<IEnumerable<EmployeeDAL>> GetEmployees()
        {
            ResponseData<IEnumerable<EmployeeDAL>> _response = new ResponseData<IEnumerable<EmployeeDAL>>();
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.GetAllEmployees, "Getting employee list");
                    _response.Data=_connection.Query<EmployeeDAL>(getAllEmployeesSqlQuery);
                    _response.returnCode = APIErrorCode.Ok;
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Getting employee list done");
                    _connection.Close();
                }
                catch(Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Getting employee exception in DAL",ex.Message);
                    _response.returnCode=APIErrorCode.DatabaseConnectionIssue;
                }       
            }

            return _response;
        }

        public ResponseData<IEnumerable<EmployeeDAL>> GetEmployee(int id)
        {
            ResponseData<IEnumerable<EmployeeDAL>> _response = new ResponseData<IEnumerable<EmployeeDAL>>();
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Getting employee by id {0}",id);
                    _response.Data = _connection.Query<EmployeeDAL>(getEmployeeByIdSqlQuery, new { Id = id });
                    _response.returnCode = APIErrorCode.Ok;
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Getting employee by id {0}", id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Getting employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;
        }

        public ResponseData<IEnumerable<EmployeeDAL>> DeleteEmployee(int id)
        {
            ResponseData<IEnumerable<EmployeeDAL>> _response = new ResponseData<IEnumerable<EmployeeDAL>>();
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.DeleteEmployee, "Deleting employee by id {0}", id);
                    _connection.Execute(deleteEmployeeByIdSqlQuery, new { Id = id });
                    _response.returnCode = APIErrorCode.NoContent;
                    _logger.LogInformation(LoggingEvents.DeleteEmployee, "Deleting employee by id {0} done", id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.DeleteEmployee, "Deleting employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;

        }

        public ResponseData<IEnumerable<EmployeeDAL>> AddEmployee(EmployeeDAL employeeToBeAdded)
        {
            ResponseData<IEnumerable<EmployeeDAL>> _response = new ResponseData<IEnumerable<EmployeeDAL>>();
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.AddEmployee, "Adding employee by id {0}", employeeToBeAdded.Id);
                    _connection.Execute(insertEmployeeSqlQuery, new { employeeToBeAdded.Id, employeeToBeAdded.Name, employeeToBeAdded.Salary });
                    _response.returnCode = APIErrorCode.Created;
                    _logger.LogInformation(LoggingEvents.AddEmployee, "Adding employee by id {0} done", employeeToBeAdded.Id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.AddEmployee, "AddEmployee employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;
        }

        public ResponseData<IEnumerable<EmployeeDAL>> UpdateEmployee(EmployeeDAL employeeToBeUpdated)
        {
            ResponseData<IEnumerable<EmployeeDAL>> _response = new ResponseData<IEnumerable<EmployeeDAL>>();
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.UpdateEmployee, "Updating employee by id {0}", employeeToBeUpdated.Id);
                    _connection.Execute(updateEmployeeSqlQuery, new { employeeToBeUpdated.Id, employeeToBeUpdated.Name, employeeToBeUpdated.Salary });
                    _response.returnCode = APIErrorCode.NoContent;
                    _logger.LogInformation(LoggingEvents.UpdateEmployee, "Adding employee by id {0} done", employeeToBeUpdated.Id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.UpdateEmployee, "AddEmployee employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;
        }
    }
}
