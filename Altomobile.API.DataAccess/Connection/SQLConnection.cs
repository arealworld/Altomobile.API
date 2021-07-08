using Altomobile.API.DataAccess.Container;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Altomobile.API.DataAccess.Connection
{
    internal class SQLConnection : IConnection<SQLConnection>
    {
        public DbConnection GetConnection()
        {
            DbConnection _connection = new SqlConnection(DataAccessContainer._configuration.GetConnectionString("SQL"));
            return _connection;
        }
    }
}
