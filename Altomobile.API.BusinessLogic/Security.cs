using Altomobile.API.DataAccess;
using Altomobile.API.DataAccess.Container;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altomobile.API.BusinessLogic
{
    public class Security : ISecurity
    {
        public async Task<string> ValidateIdentityAsync(string usr)
        {
            string retValue = string.Empty;
            try
            {
                object parammeters = new
                {
                    usr
                };

                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<ICommon>().ExecuteScalarAsync("spAM_UsersValidateIdentity", parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<bool> ValidatePasswordAsync(string guidUser, string pwd)
        {
            bool retValue = false;
            try
            {
                object parammeters = new
                {
                    guidUser,
                    pwd
                };

                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    if (!string.IsNullOrEmpty(await scope.Resolve<ICommon>().ExecuteScalarAsync("spAM_UsersValidatePassword", parammeters)))
                    {
                        retValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }
    }
}
