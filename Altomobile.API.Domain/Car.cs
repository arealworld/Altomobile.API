using System;
using System.Collections.Generic;
using System.Text;

namespace Altomobile.API.Domain
{
    public class Car
    {
        public int IdCar { get; set; }
        public string Model { get; set; }
        public int IdCarType { get; set; }
        public string CarType { get; set; }
        public int IdCarBrand { get; set; }
        public string CarBrand { get; set; }
        public int TotalRows { get; set; }
    }
}
