using Altomobile.API.Domain;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.Outputs
{
    public class SecurityAuthenticateResponse : IExamplesProvider<APIResponse>
    {
        public APIResponse GetExamples()
        {
            return new APIResponse
            {
                Err = false,
                Message = string.Empty,
                Data = "session token to access the other endpoints"
            };
        }
    }
}
