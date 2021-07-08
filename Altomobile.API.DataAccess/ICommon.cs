using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altomobile.API.DataAccess
{
    public interface ICommon
    {
        public Task<T> GetAsync<T>(string storedProcedure, object parammeters) where T : new();
        public Task<IList<T>> GetListAsync<T>(string storedProcedure, object parammeters);
        public Task<bool> ExecuteAsync(string storedProcedure, object parammeters);
        public Task<string> ExecuteScalarAsync(string storedProcedure, object parammeters);
    }
}
