using Infrastructuur.Database.Interface;
using Infrastructuur.Model;
using Microsoft.AspNetCore.Mvc;

namespace SeatleTeams.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpGet("TeamsFromUserById/{userId}")]
        public async Task<IActionResult> GetAllTeamsFromUsersAsync(int userId)
        {
           
            return Ok(await _teamService.GetAllTeamsFromUserAsync(userId));
        }
        [HttpGet("TeamById/{id}")]
        public async Task<IActionResult> GetTeamByIdAsync(int id)
        {
            return Ok(await _teamService.GetTeamByIdAsync(id));
        }
        [HttpGet("GetAllTeams")]
        public async Task<IActionResult> GetAllteams()
        {
            return Ok(await _teamService.GetTeams());
        }
        [HttpDelete("RemoveTeamById/{id}")]
        public async Task<IActionResult> RemoveTeamById(int id)
        {
            await _teamService.RemoveTeamByIdAsync(id);
            var teams = await _teamService.GetTeams();
            return Ok(teams);
        }

    }
}
