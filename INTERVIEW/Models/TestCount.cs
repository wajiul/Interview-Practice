using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace INTERVIEW.Models
{
    public class TestCount
    {
        [Key]
        public string Course { get; set; }
        public int Frequency { get; set; }
    }
}
