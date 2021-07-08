using Altomobile.API.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altomobile.API.BusinessLogic
{
    public interface ICars
    {
        public Task<IList<Car>> GetListAsync(int page, int rows, string type, string brand, string model);
        public Task<IList<Car>> GetListByTypeAsync();
        public Task<IList<Car>> GetListByTypeBrandAsync();
    }
}
