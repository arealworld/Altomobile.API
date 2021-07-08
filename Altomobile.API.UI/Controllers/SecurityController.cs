using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Altomobile.API.BusinessLogic;
using Altomobile.API.BusinessLogic.Container;
using Altomobile.API.BusinessLogic.Utils;
using Altomobile.API.Domain;
using Altomobile.API.UI.Inputs;
using Altomobile.API.UI.Outputs;
using Altomobile.API.UI.Utils;
using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;

namespace Altomobile.API.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public SecurityController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Do a simple request to the API
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Index")]
        [Produces("application/json")]
        [SwaggerResponseExample(200, typeof(StringResponse))]
        [HttpGet]
        public ActionResult<APIResponse> Index()
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                apiResponse.Data = $"Welcome to {Configuration.GetValue<string>(Constants.CONFIG_ALTOMOBILE_NAME_APPLICATION)}";

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                apiResponse.Err = true;
                apiResponse.Message = ex.Message;

                return StatusCode(500,apiResponse);
            }
        }

        /// <summary>
        /// Verify if authorization works when session token has already been obtained
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("Verify")]
        [Produces("application/json")]
        [SwaggerResponseExample(200, typeof(StringResponse))]
        [HttpGet]
        public ActionResult<APIResponse> Verify()
        {
            APIResponse apiResponse = new APIResponse();
            try
            {
                apiResponse.Data = "Authorization works!";

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
        /// Authenticate a user and get a session token to access the other endpoints
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Authenticate")]
        [Produces("application/json")]
        [SwaggerResponseExample(200, typeof(SecurityAuthenticateResponse))]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Authenticate(SecurityAuthenticateRequest request)
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
                    User user = null;
                    string guidUser = string.Empty;

                    guidUser = await scope.Resolve<ISecurity>().ValidateIdentityAsync(request.user);

                    if (string.IsNullOrEmpty(guidUser))
                    {
                        throw new Exception("User does not exist.");
                    }

                    if (!await scope.Resolve<ISecurity>().ValidatePasswordAsync(guidUser, request.password))
                    {
                        throw new Exception("Password is invalid.");
                    }

                    user = await scope.Resolve<IUsers>().GetAsync(guidUser);

                    if (user == null)
                    {
                        throw new Exception("Login failed.");
                    }

                    apiResponse.Data = GenerateToken(user);
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

        private string GenerateToken(User user)
        {
            byte[] jwtKey = Encoding.ASCII.GetBytes(Configuration.GetValue<string>(Constants.CONFIG_ALTOMOBILE_JWT_KEY));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("guid", user.guid ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("firstname", user.firstName ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("lastname", user.lastName ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("usr", user.usr ?? string.Empty));
            claimsIdentity.AddClaim(new Claim("active", user.active.ToString(), ClaimValueTypes.Boolean));

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtKey), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}
