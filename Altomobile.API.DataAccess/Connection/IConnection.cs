using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Altomobile.API.DataAccess.Connection
{
    internal interface IConnection<T> where T: class
    {
        public DbConnection GetConnection();
    }
}
