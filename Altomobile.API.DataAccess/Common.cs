using Altomobile.API.DataAccess.Connection;
using Altomobile.API.DataAccess.Container;
using Autofac;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Altomobile.API.DataAccess
{
    public class Common: ICommon
    {
        public async Task<T> GetAsync<T>(string storedProcedure, object parammeters) where T : new()
        {
            T retValue = new T();
            DbConnection _connection = null;
            try
            {
                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IConnection<SQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    retValue = await _connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue;
        }

        public async Task<IList<T>> GetListAsync<T>(string storedProcedure, object parammeters)
        {
            List<T> retValue = new List<T>();
            DbConnection _connection = null;
            try
            {
                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IConnection<SQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    retValue = (List<T>)await _connection.QueryAsync<T>(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                    if (retValue != null && retValue.Count == 0)
                    {
                        retValue = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue;
        }

        public async Task<bool> ExecuteAsync(string storedProcedure, object parammeters)
        {
            bool retValue = false;
            DbConnection _connection = null;
            try
            {
                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IConnection<SQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    if (await _connection.ExecuteAsync(storedProcedure, parammeters, commandType: CommandType.StoredProcedure) > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue;
        }

        public async Task<string> ExecuteScalarAsync(string storedProcedure, object parammeters)
        {
            string retValue = string.Empty;
            DbConnection _connection = null;
            try
            {
                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    _connection = scope.Resolve<IConnection<SQLConnection>>().GetConnection();
                }

                using (_connection)
                {
                    await _connection.OpenAsync();
                    retValue = await _connection.QueryFirstOrDefaultAsync<string>(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_connection != null && _connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                    _connection.Dispose();
                }
            }
            return retValue ?? string.Empty;
        }
    }
}
