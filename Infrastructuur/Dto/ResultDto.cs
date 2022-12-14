using Infrastructuur.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructuur.Dto
{
    public class ResultDto 
    {
        public List<string>? Errors { get; set; } = new List<string>();
        public string? Name { get; set; }
    }
}
