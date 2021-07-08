using Altomobile.API.Domain;
using Altomobile.API.UI.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.Utils
{
    public class Mapper: Profile
    {

        public Mapper()
        {
            //to optimize the mapping of values from one class to another
            CreateMap<Car, DTOCar>();
        }
    }
}
