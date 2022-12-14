using Infrastructuur.Database.Interface;
using Infrastructuur.Database.Mongo;
using Infrastructuur.Dto;
using Infrastructuur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructuur.Mappers;

namespace Infrastructuur.Database.Classes
{
    public class TeamService : ITeamService
    {
        private readonly MongoDbContext _mongoDbContext;

        public TeamService(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public Task<bool> AddTeamAsync(TeamEntity team)
        {
            throw new NotImplementedException();
        }

        public Task<List<TeamEntity>> GetAllTeamsFromUserAsync(int userId)
        {
            return _mongoDbContext.GetAllTeamsFromUser(userId);
        }

        public async Task<ResultDto> GetTeamByIdAsync(int teamId)
        {
            var resultDto = new ResultDto();
            var team = await _mongoDbContext.GetByIdAsync<TeamEntity>(x => x.Id == teamId, "Team");
            if (team is null)
            {
                resultDto.Errors.Add($"No team with id {teamId} found!");
                return resultDto;
            }
            resultDto = team.MapToTeamDto();
            return resultDto;
        }

        public async Task<List<TeamEntity>> GetTeams()
        {
            return (await _mongoDbContext.GetAllAsync<TeamEntity>("Team")).ToList();
        }

        public Task<List<TeamEntity>> GetTeamsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveTeamByIdAsync(int teamId)
        {
            return await _mongoDbContext.DeleteAsync<TeamEntity>(teamId,"Team", x => x.Id == teamId);
        }
    }
}
