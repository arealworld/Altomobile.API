using System;
using System.Collections.Generic;
using System.Text;

namespace Altomobile.API.Domain
{
    public class APIResponse
    {
        public bool Err { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; } = null;
    }
}
