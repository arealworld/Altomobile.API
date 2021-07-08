﻿using Altomobile.API.Domain;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.Outputs
{
    public class CarsReportResponse : IExamplesProvider<APIResponse>
    {
        public APIResponse GetExamples()
        {
            return new APIResponse
            {
                Err = false,
                Message = string.Empty,
                Data = "object according to the type of report"
            };
        }
    }
}
