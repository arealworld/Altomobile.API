using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Altomobile.API.BusinessLogic;
using Altomobile.API.BusinessLogic.Container;
using Altomobile.API.BusinessLogic.Utils;
using Altomobile.API.Domain;
using Altomobile.API.UI.DTO;
using Altomobile.API.UI.Inputs;
using Altomobile.API.UI.Outputs;
using Altomobile.API.UI.Utils;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Filters;

namespace Altomobile.API.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IMapper Mapper;

        public CarsController(IConfiguration configuration, IMapper mapper)
        {
            Configuration = configuration;
            Mapper = mapper;
        }

        /// <summary>
        /// Search cars endpoint
        /// </summary>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="type"></param>
        /// <param name="brand"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [Route("GetList")]
        [Produces("application/json")]
        [SwaggerResponseExample(200, typeof(CarsListResponse))]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetList(
            [Required][Range(1, int.MaxValue)] int page,
            [Required][Range(1, int.MaxValue)] int rows,
            [StringLength(100)] string type,
            [StringLength(100)] string brand,
            [StringLength(100)] string model)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    apiResponse.Err = true;
                    apiResponse.Message = Helper.ErrorsToString(ModelState);

                    return BadRequest(apiResponse);
                }

                using (var scope = Container._container.BeginLifetimeScope())
                {
                    List<Car> carsList = (List<Car>)await scope.Resolve<ICars>().GetListAsync(page, rows, type, brand, model);

                    if (carsList == null)
                    {
                        apiResponse.Message = "No results, try other search criteria.";
                    }
                    else
                    {
                        DTOCarsList cars = new DTOCarsList()
                        {
                            TotalRows = carsList.FirstOrDefault().TotalRows,
                            Cars = Mapper.Map<List<DTOCar>>(carsList)
                        };

                        apiResponse.Data = cars;
                    }
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Err = true;
                apiResponse.Message = ex.Message;

                return StatusCode(500, apiResponse);
            }
        }

        /// <summary>
        /// Reporting endpoint
        /// </summary>
        /// <param name="carReportType"></param>
        /// <returns></returns>
        [Authorize]
        [Route("GetReport")]
        [Produces("application/json")]
        [SwaggerResponseExample(200, typeof(CarsReportResponse))]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetReport([Required] Enums.CarReportType carReportType)
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                if (!ModelState.IsValid)
                {
                    apiResponse.Err = true;
                    apiResponse.Message = Helper.ErrorsToString(ModelState);

                    return BadRequest(apiResponse);
                }

                using (var scope = Container._container.BeginLifetimeScope())
                {
                    List<Car> carsList = new List<Car>();

                    switch (carReportType)
                    {
                        case Enums.CarReportType.Type:

                            carsList = (List<Car>)await scope.Resolve<ICars>().GetListByTypeAsync();

                            apiResponse.Data = carsList
                                .Select(x => new { x.CarType, x.TotalRows })
                                .ToDictionary(x => x.CarType, x => x.TotalRows);

                            break;

                        case Enums.CarReportType.TypeBrand:

                            carsList = (List<Car>)await scope.Resolve<ICars>().GetListByTypeBrandAsync();

                            var carsListByType = carsList.GroupBy(x => new { x.CarType });

                            apiResponse.Data = carsListByType
                                .Select(x => new { x.Key.CarType, CardBrand = x.ToDictionary(y => y.CarBrand, y => y.TotalRows) })
                                .ToDictionary(x => x.CarType, x => x.CardBrand);

                            break;

                        case Enums.CarReportType.BrandType:

                            carsList = (List<Car>)await scope.Resolve<ICars>().GetListByTypeBrandAsync();

                            var carsListByBrand = carsList.GroupBy(x => new { x.CarBrand });

                            apiResponse.Data = carsListByBrand
                                .Select(x => new { x.Key.CarBrand, CardType = x.ToDictionary(y => y.CarType, y => y.TotalRows) })
                                .ToDictionary(x => x.CarBrand, x => x.CardType);

                            break;

                        default:

                            apiResponse.Err = true;
                            apiResponse.Message = "Available values to create report: 1(Type), 2(Type and Brand), 3(Brand and Type)";

                            return BadRequest(apiResponse);
                    }
                }

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Err = true;
                apiResponse.Message = ex.Message;

                return StatusCode(500, apiResponse);
            }
        }
    }
}
