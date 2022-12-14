using Infrastructuur.Dto;
using Infrastructuur.Model;

namespace Infrastructuur.Database.Interface
{
    public interface ITeamService
    {
        Task<List<TeamEntity>> GetAllTeamsFromUserAsync(int userId);
        Task<List<TeamEntity>> GetTeams();
        Task<ResultDto> GetTeamByIdAsync(int teamId);
        Task<bool> AddTeamAsync(TeamEntity team);
        Task<bool> RemoveTeamByIdAsync(int teamId);
    }
}
