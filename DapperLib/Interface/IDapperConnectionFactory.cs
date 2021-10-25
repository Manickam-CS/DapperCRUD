using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DapperLib.Interface
{
    public interface IDapperConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void CloseConnection();     
    }
}
