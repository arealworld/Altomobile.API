using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.DTO
{
    public class DTOCar
    {
        public int IdCar { get; set; }
        public string Model { get; set; }
        public int IdCarType { get; set; }
        public string CarType { get; set; }
        public int IdCarBrand { get; set; }
        public string CarBrand { get; set; }
    }
}
