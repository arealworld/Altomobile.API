using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Altomobile.API.BusinessLogic
{
    public interface ISecurity
    {
        public Task<string> ValidateIdentityAsync(string usr);
        public Task<bool> ValidatePasswordAsync(string guidUser, string pwd);
    }
}
