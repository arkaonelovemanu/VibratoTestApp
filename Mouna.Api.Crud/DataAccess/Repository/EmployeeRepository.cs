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
        private ResponseData<IEnumerable<EmployeeDAL>> _response = new ResponseData<IEnumerable<EmployeeDAL>>();

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
            
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.GetAllEmployees, "Repository:Getting all employees");
                    _response.Data=_connection.Query<EmployeeDAL>(getAllEmployeesSqlQuery);
                    _response.returnCode = APIErrorCode.Ok;
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Repository:Getting all employee done");
                    _connection.Close();
                }
                catch(Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Repository:Getting employee exception in DAL", ex.Message);
                    _response.returnCode=APIErrorCode.DatabaseConnectionIssue;
                }       
            }

            return _response;
        }

        public ResponseData<IEnumerable<EmployeeDAL>> GetEmployee(int id)
        {           
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Repository:Getting employee by id {0}", id);
                    _response.Data = _connection.Query<EmployeeDAL>(getEmployeeByIdSqlQuery, new { Id = id });
                    _response.returnCode = APIErrorCode.Ok;
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Repository:Getting employee by id {0}", id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.GetEmployeeById, "Repository:Getting employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;
        }

        public ResponseData<IEnumerable<EmployeeDAL>> DeleteEmployee(int id)
        {
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.DeleteEmployee, "Repository:Deleting employee by id {0}", id);
                    _connection.Execute(deleteEmployeeByIdSqlQuery, new { Id = id });
                    _response.returnCode = APIErrorCode.NoContent;
                    _logger.LogInformation(LoggingEvents.DeleteEmployee, "Repository:Deleting employee by id {0} done", id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.DeleteEmployee, "Repository:Deleting employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;

        }

        public ResponseData<IEnumerable<EmployeeDAL>> AddEmployee(EmployeeDAL employeeToBeAdded)
        {
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.AddEmployee, "Repository:Adding employee by id {0}", employeeToBeAdded.Id);
                    _connection.Execute(insertEmployeeSqlQuery, new { employeeToBeAdded.Id, employeeToBeAdded.Name, employeeToBeAdded.Salary });
                    _response.Data = Enumerable.Repeat(employeeToBeAdded, 1);
                    _response.returnCode = APIErrorCode.Created;
                    _logger.LogInformation(LoggingEvents.AddEmployee, "Repository:Adding employee by id {0} done", employeeToBeAdded.Id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.AddEmployee, "Repository:AddEmployee employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;
        }

        public ResponseData<IEnumerable<EmployeeDAL>> UpdateEmployee(EmployeeDAL employeeToBeUpdated)
        {
            using (IDbConnection _connection = Connection)
            {
                try
                {
                    _connection.Open();
                    _logger.LogInformation(LoggingEvents.UpdateEmployee, "Repository:Updating employee by id {0}", employeeToBeUpdated.Id);
                    _connection.Execute(updateEmployeeSqlQuery, new { employeeToBeUpdated.Id, employeeToBeUpdated.Name, employeeToBeUpdated.Salary });
                    _response.Data = Enumerable.Repeat(employeeToBeUpdated, 1);
                    _response.returnCode = APIErrorCode.NoContent;
                    _logger.LogInformation(LoggingEvents.UpdateEmployee, "Repository:Updating employee by id {0} done", employeeToBeUpdated.Id);
                    _connection.Close();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(LoggingEvents.UpdateEmployee, "Repository:Updating employee by id exception", ex.Message);
                    _response.returnCode = APIErrorCode.DatabaseConnectionIssue;
                }
            }
            return _response;
        }
    }
}
