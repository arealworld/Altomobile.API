using Altomobile.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.DTO
{
    public class DTOCarsList
    {
        public List<DTOCar> Cars { get; set; }
        public int TotalRows { get; set; }
    }
}
