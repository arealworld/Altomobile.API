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
    public class Cars : ICars
    {
        public async Task<IList<Car>> GetListAsync(int page, int rows, string type, string brand, string model)
        {
            List<Car> retValue = null;
            try
            {
                object parammeters = new
                {
                    page,
                    rows,
                    type,
                    brand,
                    model
                };

                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    retValue = (List<Car>)await scope.Resolve<ICommon>().GetListAsync<Car>("spAM_CarsGetList", parammeters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<IList<Car>> GetListByTypeAsync()
        {
            List<Car> retValue = null;
            try
            {
                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    retValue = (List<Car>)await scope.Resolve<ICommon>().GetListAsync<Car>("spAM_CarsGetListByType", null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retValue;
        }

        public async Task<IList<Car>> GetListByTypeBrandAsync()
        {
            List<Car> retValue = null;
            try
            {
                using (var scope = DataAccessContainer._container.BeginLifetimeScope())
                {
                    retValue = (List<Car>)await scope.Resolve<ICommon>().GetListAsync<Car>("spAM_CarsGetListByTypeBrand", null);
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
