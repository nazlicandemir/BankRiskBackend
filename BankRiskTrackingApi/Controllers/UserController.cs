using BankRiskTracking.Entities.DTOs;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankRiskTrackingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public IActionResult CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            if(userCreateDto == null)
            {
                return BadRequest("Kullanıcı bilgileri boş olamaz");
            }
            var result = _userService.CreateUser(userCreateDto);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpPost("Login")]

        public IActionResult LoginUser([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
            {
                return BadRequest("Kullanıcı bilgileri boş olamaz");
            }
            var result = _userService.LoginUser(userLoginDto);
            if (result.IsSuccess)
            { 
                return Ok(new { token = result.Data, message = result.Message });
            }
                return BadRequest(result.Message);

        }
    }
}
