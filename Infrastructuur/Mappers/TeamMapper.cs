using Infrastructuur.Dto;
using Infrastructuur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Mappers
{
    public static class TeamMapper
    {
        public static ResultDto MapToTeamDto(this TeamEntity team)
        {
            return new ResultDto
            {
               Name = team.Name
            };
        }
    }
}
