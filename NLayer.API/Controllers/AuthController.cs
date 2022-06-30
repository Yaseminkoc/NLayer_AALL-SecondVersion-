using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NLayer.Bussiness.Abstract;
using NLayer.Core.Results.Concrete;
using NLayer.Entity.Concrete;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NLayer.Entity.Dto;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private ILoggerService _logger;
        public AuthController(IUserService userService, IConfiguration configuration, IRoleService roleService)
        {
            _userService = userService;
            _configuration = configuration;
            _roleService = roleService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto);
            if (result.Success)
            {
                ApplicationUser user = result.Data;
                var rolesResult = await _roleService.GetUserRolesAsync(user.Id);
                if (rolesResult.Success)
                {
                    var roles = rolesResult.Data;
                    string Token = GenerateToken(user, roles);
                    dynamic x = new ExpandoObject();
                    x.token = Token;
                    x.roles = roles;
                    x.displayName = result.Data.DisplayName;
                  
                    return Ok(JsonConvert.SerializeObject(x));
                }
                else
                {
                    _logger.LogError("rolesResult.Success() has been crashed");
                    return BadRequest(rolesResult.Message);
                }
            }
            else
            {
                if (result.GetType() == typeof(ValidationErrorResult))
                {
                    foreach (var modelValue in ModelState.Values)
                    {
                        modelValue.Errors.Clear();
                    }

                    ValidationErrorResult validationErrorResult = (ValidationErrorResult)result;
                    foreach (var item in validationErrorResult.GetValidationFailures())
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }

                    return UnprocessableEntity(ModelState);
                }
                else
                {
                  
                    return BadRequest(result.Message);
                }
            }
        }

        private string GenerateToken(ApplicationUser user, List<string> roles)
        {
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "TestIssuer",
                Audience = "TestAudience"
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
