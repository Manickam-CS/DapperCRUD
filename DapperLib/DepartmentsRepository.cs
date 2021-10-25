using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Dapper;
using DapperLib.Models;
using DapperLib.Interface;

namespace DapperLib
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        IDapperConnectionFactory _connectionFactory;
        public DepartmentsRepository( IDapperConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IList<Department> GetDepartmentsByQuery()
        {
            List<Department> departments = new List<Department>();
            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                departments = conn.Query<Department>("Select * From Departments").ToList();
                return departments;
            }          
        }

        public Department GetDepartmentsById(int deptId)
        {
            var SqlQuery = @"Select * From Departments WHERE deptId = @deptId";
            var param = new DynamicParameters();
            param.Add("@deptId", deptId);

            using (IDbConnection conn = _connectionFactory.GetConnection)
            {
                var department = conn.QueryFirstOrDefault<Department>(SqlQuery, param);
                return department;
            }
        }

        public bool AddDepartment(Department department)
        {
            string procName = "usp_AddDepartment";
            var param = new DynamicParameters();
            //int deptId = 0;
            //param.Add("@DeptId", department.DeptId, null, ParameterDirection.Output);
            bool IsSuccess = false;
            param.Add("@Name", department.Name);
            try
            {
                var rowsAffected = SqlMapper.Execute(_connectionFactory.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure);
                //deptId = param.Get<int>("@DeptId");
                if (rowsAffected > 0)
                {
                    IsSuccess = true;
                }
            }
            finally
            {
                _connectionFactory.CloseConnection();
            }
            return IsSuccess;
        }

        public bool UpdateDepartment(Department department)
        {
            string procName = "usp_UpdateDepartment";
            var param = new DynamicParameters();
            bool IsSuccess = false;
            param.Add("@DeptId", department.DeptId);
            param.Add("@Name", department.Name);     
            try
            {
                var rowsAffected = SqlMapper.Execute(_connectionFactory.GetConnection,
                procName, param, commandType: CommandType.StoredProcedure);
                if (rowsAffected > 0)
                {
                    IsSuccess = true;
                }
            }
            finally
            {
                _connectionFactory.CloseConnection();
            }

            return IsSuccess;
        }

        public bool DeleteDepartment(int deptId)
        {
            bool IsDeleted = true;
            var SqlQuery = @"Delete From Departments WHERE deptId = @deptId";
            var param = new DynamicParameters();
            param.Add("@deptId", deptId);

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
