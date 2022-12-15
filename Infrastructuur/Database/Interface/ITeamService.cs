using Infrastructuur.Dto;
using Infrastructuur.Model;

namespace Infrastructuur.Database.Interface
{
    public interface ITeamService
    {
        Task<List<TeamEntity>> GetAllTeamsFromUserAsync(int userId);
        Task<List<TeamEntity>> GetTeamsAsync();
        Task<ResultDto> GetTeamByIdAsync(int teamId);
        Task<TeamEntity> AddTeamAsync(TeamEntity team);
        Task<bool> RemoveTeamByIdAsync(int teamId);
        Task<bool> UpdateTeamById(int teamId, string name);
    }
}
