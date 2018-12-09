using Mouna.Api.Crud.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Mouna.Api.Crud.DataAccess.Interfaces;

namespace Mouna.Api.Crud.DataAccess.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private IDbConnection _connection;

        const string getAllEmployeesSqlQuery = "SELECT * FROM employeeForCRUD";
        const string getEmployeeByIdSqlQuery = "SELECT * FROM employeeForCRUD where Id= @Id";
        const string deleteEmployeeByIdSqlQuery = "DELETE FROM employeeForCRUD WHERE Id = @Id";
        const string insertEmployeeSqlQuery = "INSERT INTO employeeForCRUD Values (@Id,@Name,@Salary);";
        const string updateEmployeeSqlQuery = "UPDATE employeeForCRUD SET Name = @Name, Salary=@Salary WHERE Id = @Id;";
        public EmployeeRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<EmployeeDAL> GetEmployees()
        {

                return _connection.Query<EmployeeDAL>(getAllEmployeesSqlQuery);
        }

        public IEnumerable<EmployeeDAL> GetEmployee(int id)
        {
                return _connection.Query<EmployeeDAL>(getEmployeeByIdSqlQuery, new { Id = id });
        }

        public int DeleteEmployee(int id)
        {

            return _connection.Execute(deleteEmployeeByIdSqlQuery, new { Id = id });

        }

        public int AddEmployee(EmployeeDAL employeeToBeAdded)
        {

                return _connection.Execute(insertEmployeeSqlQuery, new { employeeToBeAdded.Id, employeeToBeAdded.Name, employeeToBeAdded.Salary });
        }

        public int UpdateEmployee(EmployeeDAL employeeToBeUpdated)
        {
                _connection.Open();
                return _connection.Execute(updateEmployeeSqlQuery, new { employeeToBeUpdated.Id, employeeToBeUpdated.Name, employeeToBeUpdated.Salary });
        }
    }
}
