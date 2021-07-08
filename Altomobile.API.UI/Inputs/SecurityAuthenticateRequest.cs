﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.Inputs
{
    public class SecurityAuthenticateRequest
    {
        [Required]
        [StringLength(100)]
        public string user { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }
    }
}
