using Infrastructuur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Database.Interface
{
    public interface IUserService
    {
        Task<List<UserEntity>> GetUsersAsync();
        Task<UserEntity> GetUserByIdAsync(int userId);
        Task<UserEntity> AddUserAsync(UserEntity userId);
        Task<bool> RemoveUserByIdAsync(int userId);
        Task<bool> UpdateUserById(int userId, string name);
    }
}
