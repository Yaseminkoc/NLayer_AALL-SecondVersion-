using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLayer.Bussiness.Abstract;
using NLayer.Entity.Dto;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private ILoggerService _logger;
        public TeacherController(ILoggerService logger, IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
            _logger = logger; //DI , constructor injection

        }


        [HttpGet("me")]
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<IActionResult> Me()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;
            var username = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var userResult = await _userService.GetUserByUsername(new UsernameDto(){Username = username});
            if (userResult.Success)
            {
                var user = userResult.Data;
                var roleResult = await _roleService.GetUserRolesAsync(user.Id);
                if (roleResult.Success)
                {
                    var roles = roleResult.Data;
                    _logger.LogInfo("Projects.Success() has been run");
                    return Ok(new { user = user, roles = roles });
                }
                else
                {
                    _logger.LogError("Projects.Success() has been crashed");
                    return BadRequest(roleResult.Message);
                }
            }
            else
            {
                _logger.LogError("Projects.Success() has been crashed");
                return BadRequest(userResult.Message);
            }
        }
        
    }
}
