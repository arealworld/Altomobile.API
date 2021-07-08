using Altomobile.API.DataAccess;
using Altomobile.API.DataAccess.Container;
using Altomobile.API.Domain;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altomobile.API.BusinessLogic
{
    public class Users : IUsers
    {
        public async Task<User> GetAsync(string guidUser)
        {
            User retValue = null;
            try
            {
                object parammeters = new
                {
                    guidUser
                };

                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    retValue = await scope.Resolve<ICommon>().GetAsync<User>("spAM_UsersGet", parammeters);
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
