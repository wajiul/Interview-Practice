using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTERVIEW.Models
{
    public class NewQuestion
    {
        public string Course { get; set; }
        public string Question { get; set; }
        public int Rank { get; set; } = 1;
    }
}
