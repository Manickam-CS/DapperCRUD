using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DapperLib.Interface;
using DapperLib.Models;

namespace DapperLib
{
    public class EmployeesRepository : IEmployeesRepository
    {
        IDapperConnectionFactory _connectionFactory;
        public EmployeesRepository(IDapperConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IList<Employee> GetEmployeesByQuery()
        {
            var SqlQuery = "SELECT EMP.[EmpId],EMP.[Name],EMP.[Address],EMP.[ImagePath],EMP.[DeptId],DEP.[Name] AS DepartmentName  FROM [dbo].[Employees] EMP INNER JOIN [dbo].[Departments] DEP ON EMP.DeptId = DEP.DeptId";
            List<Employee> employees = new List<Employee>();
            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                employees = conn.Query<Employee>(SqlQuery).ToList();               
            }
            return employees;
        }

        public Employee GetEmployeesById(int empId)
        {
            //var SqlQuery = @"Select * From Employees WHERE empId = @empId";
            var SqlQuery = "SELECT EMP.[EmpId],EMP.[Name],EMP.[Address],EMP.[ImagePath],EMP.[DeptId], DEP.[Name] AS DepartmentName  FROM [dbo].[Employees] EMP INNER JOIN [dbo].[Departments] DEP ON EMP.DeptId = DEP.DeptId WHERE  EMP.[EmpId] =  @empId";
            var param = new DynamicParameters();
            param.Add("@EmpId", empId);
            Employee employee = null;
            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                employee = conn.QueryFirstOrDefault<Employee>(SqlQuery, param);               
            }
            return employee;
        }

        public bool AddEmployee(Employee employee)
        {
            string procName = "usp_AddEmployee";
            var param = new DynamicParameters();
            bool IsSuccess = true;
            param.Add("@EmpId", employee.EmpId, null, ParameterDirection.Output);
            param.Add("@Name", employee.Name);
            param.Add("@Address", employee.Address);
            param.Add("@ImagePath", employee.ImagePath);
            param.Add("@DeptId", employee.DeptId);
            try
            {
                using (IDbConnection conn = _connectionFactory.GetConnection)
                {
                    var rowsAffected = SqlMapper.Execute(conn, procName, param, commandType: CommandType.StoredProcedure);
                    if (rowsAffected <= 0)
                    {
                        IsSuccess = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            //finally
            //{
            //    _connectionFactory.CloseConnection();
            //}
            return IsSuccess;
        }

        public bool UpdateEmployee(Employee employee)
        {
            string procName = "usp_UpdateEmployee";
            var param = new DynamicParameters();
            bool IsSuccess = false;
            param.Add("@EmpId", employee.EmpId);
            param.Add("@Name", employee.Name);
            param.Add("@Address", employee.Address);
            param.Add("@ImagePath", employee.ImagePath);
            param.Add("@DeptId", employee.DeptId);
            try
            {
                using (IDbConnection conn = _connectionFactory.GetConnection)
                {
                    var rowsAffected = SqlMapper.Execute(conn, procName, param, commandType: CommandType.StoredProcedure);
                    if (rowsAffected <= 0)
                    {
                        IsSuccess = true;
                    }
                }
            }
            catch(Exception ex)
            {
              
            }

            return IsSuccess;
        }

        public bool DeleteEmployee(int empId)
        {
            bool IsDeleted = true;
            var SqlQuery = @"Delete From [dbo].[Employees] WHERE EmpId = @empId";
            var param = new DynamicParameters();
            param.Add("@empId", empId);
           
            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                var rowsaffected = conn.Execute(SqlQuery, param);
                if (rowsaffected <= 0)
                {
                    IsDeleted = false;
                }
            }
            return IsDeleted;
        }
    }
}
