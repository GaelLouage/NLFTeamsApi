using Infrastructuur.Database.Interface;
using Infrastructuur.Model;
using Microsoft.AspNetCore.Mvc;

namespace SeatleTeams.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
           
            return Ok(await _userService.GetUsersAsync());
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }
        [HttpDelete("DeleteUserById/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if(!await _userService.RemoveUserByIdAsync(id))
            {
                return NotFound("User not deleted");
            }
            return Ok(await _userService.GetUsersAsync());
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserEntity user)
        {
            return Ok(await _userService.AddUserAsync(user));
        }
        [HttpPut("UpdateUserbyId/{id}")]
        public async Task<IActionResult> UpdateUser(int id, string name)
        {
            await _userService.UpdateUserById(id, name);
            return Ok(await _userService.GetUsersAsync());
        }
    }
}
