using Altomobile.API.BusinessLogic.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.Inputs
{
    public class CarsReportRequest
    {
        /// <summary>
        /// Available values to create report: 1(Type), 2(Type and Brand), 3(Brand and Type)
        /// </summary>
        [Required]
        public int carReportType { get; set; }
    }
}
