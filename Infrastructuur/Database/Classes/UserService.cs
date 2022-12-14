using Infrastructuur.Database.Interface;
using Infrastructuur.Database.Mongo;
using Infrastructuur.Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Database.Classes
{
    public class UserService : IUserService
    {
        private readonly MongoDbContext _mongoDbContext;
        private readonly ITeamService _teamService;
        public UserService(MongoDbContext mongoDbContext, ITeamService teamService)
        {
            _mongoDbContext = mongoDbContext;
            _teamService = teamService;
        }

        public async Task<UserEntity> AddUserAsync(UserEntity user)
        {
            return await _mongoDbContext.CreateAsync<UserEntity>(user, "User",
                new BsonDocument
                {
                    {"id", (await GetUsersAsync()).Max(x => x.Id) + 1},
                    {"name",user.Name },
                 }
                );
        }
        public async Task<List<UserEntity>> GetUsersAsync()
        {
            var users = (await _mongoDbContext.GetAllAsync<UserEntity>("User")).ToList();
            foreach (var user in users)
            {
                user.UserTeams = await _teamService.GetAllTeamsFromUserAsync(user.Id);
            }
            return users;
        }

        public async Task<UserEntity> GetUserByIdAsync(int userId)
        {
            var user = await _mongoDbContext.GetByIdAsync<UserEntity>(x => x.Id == userId, "User");
            user.UserTeams = await _mongoDbContext.GetAllTeamsFromUser(userId);
            return user;
        }

        public async Task<bool> RemoveUserByIdAsync(int userId)
        {
            return await _mongoDbContext.DeleteAsync<UserEntity>(userId, "User", x => x.Id == userId);
        }

        public async Task<bool> UpdateUserById(int userId, string name)
        {
            return await _mongoDbContext.UpdateAsync<UserEntity>(
                userId,
                "name",
                name,
                "User",
                new BsonDocument
                {
                    {"name",name }
                }
                );
        }
    }
}
