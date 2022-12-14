using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Model
{
    public class TeamEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserEntity>? UserTeams { get; set; }
    }
}
