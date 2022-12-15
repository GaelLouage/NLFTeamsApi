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
using MongoDB.Bson;

namespace Infrastructuur.Database.Classes
{
    public class TeamService : ITeamService
    {
        private readonly MongoDbContext _mongoDbContext;

        public TeamService(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<TeamEntity> AddTeamAsync(TeamEntity team)
        {
            return await _mongoDbContext.CreateAsync<TeamEntity>(team, "Team",
                 new BsonDocument
                 {
                    {"id", (await GetTeamsAsync()).Max(x => x.Id) + 1},
                    {"name",team.Name },
                  }
                 );
        }

        public async Task<List<TeamEntity>> GetAllTeamsFromUserAsync(int userId)
        {
            return await _mongoDbContext.GetAllTeamsFromUser(userId);
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

        public async Task<List<TeamEntity>> GetTeamsAsync()
        {
            return (await _mongoDbContext.GetAllAsync<TeamEntity>("Team")).ToList();
        }


        public async Task<bool> RemoveTeamByIdAsync(int teamId)
        {
            return await _mongoDbContext.DeleteAsync<TeamEntity>(teamId,"Team", x => x.Id == teamId);
        }

        public async Task<bool> UpdateTeamById(int teamId, string name)
        {
            return await _mongoDbContext.UpdateAsync<TeamEntity>(
                teamId,
                "name",
                name,
                "Team",
                new BsonDocument
                {
                    {"name",name }
                }
                );
        }
    }
}
