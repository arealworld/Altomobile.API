using Altomobile.API.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altomobile.API.BusinessLogic
{
    public interface IUsers
    {
        public Task<User> GetAsync(string guidUser);
    }
}
