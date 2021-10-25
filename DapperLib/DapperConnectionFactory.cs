using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DapperLib.Interface;
using DapperLib.Models;

namespace DapperLib
{
    public class DapperConnectionFactory : IDapperConnectionFactory
    {
        private IDbConnection _connection;
        private readonly IOptions<EmployeeDBConfiguration> _configs;

        public DapperConnectionFactory(IOptions<EmployeeDBConfiguration> Configs)
        {
            _configs = Configs;
        }
        public IDbConnection GetConnection 
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_configs.Value.DbConnection);
                }
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                return _connection;
            }          
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
