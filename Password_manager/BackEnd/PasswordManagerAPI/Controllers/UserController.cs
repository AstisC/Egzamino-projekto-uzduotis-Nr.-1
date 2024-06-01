using Microsoft.AspNetCore.Mvc;
using PasswordManagerAPI.Models;
using PasswordManagerAPI.Services;
using System.Threading.Tasks;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var result = await _userService.RegisterUserAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            var result = await _userService.LoginUserAsync(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }

        [HttpPost("password")]
        public async Task<IActionResult> AddPasswordEntry([FromBody] PasswordEntryModel model)
        {
            var result = await _userService.AddPasswordEntryAsync(model.UserId, model.SiteName, model.Password);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("password/{id}")]
        public async Task<IActionResult> UpdatePasswordEntry(int id, [FromBody] string newPassword)
        {
            var result = await _userService.UpdatePasswordEntryAsync(id, newPassword);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("password/{id}")]
        public async Task<IActionResult> DeletePasswordEntry(int id)
        {
            var result = await _userService.DeletePasswordEntryAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("password/{id}")]
        public async Task<IActionResult> GetPasswordEntry(int id)
        {
            var entry = await _userService.GetPasswordEntryAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpGet("password/user/{userId}")]
        public async Task<IActionResult> GetUserPasswordEntries(int userId)
        {
            var entries = await _userService.GetUserPasswordEntriesAsync(userId);
            return Ok(entries);
        }
    }

    public class PasswordEntryModel
    {
        public int UserId { get; set; }
        public string SiteName { get; set; }
        public string Password { get; set; }
    }
}
