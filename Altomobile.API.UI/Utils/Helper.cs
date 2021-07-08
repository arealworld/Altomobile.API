using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.Utils
{
    public static class Helper
    {
        public static string ErrorsToString(ModelStateDictionary modelState)
        {
            return string.Join(string.Empty, modelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList());
        }
    }
}
