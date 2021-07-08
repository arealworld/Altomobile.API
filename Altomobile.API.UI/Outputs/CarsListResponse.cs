using Altomobile.API.Domain;
using Altomobile.API.UI.DTO;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altomobile.API.UI.Outputs
{
    public class CarsListResponse : IExamplesProvider<APIResponse>
    {
        public APIResponse GetExamples()
        {
            return new APIResponse
            {
                Err = false,
                Message = string.Empty,
                Data = new DTOCarsList()
                {
                    Cars = new List<DTOCar>()
                    {
                        new DTOCar()
                        {
                            IdCar = 1,
                            Model = "example",
                            IdCarType = 1,
                            CarType = "example",
                            IdCarBrand = 1,
                            CarBrand = "example"
                        },
                        new DTOCar()
                        {
                            IdCar = 2,
                            Model = "example",
                            IdCarType = 1,
                            CarType = "example",
                            IdCarBrand = 1,
                            CarBrand = "example"
                        },
                        new DTOCar()
                        {
                            IdCar = 3,
                            Model = "example",
                            IdCarType = 1,
                            CarType = "example",
                            IdCarBrand = 1,
                            CarBrand = "example"
                        },
                    },
                    TotalRows = 3
                }
            };
        }
    }
}
