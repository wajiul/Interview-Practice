using INTERVIEW.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTERVIEW.Data
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        public DbSet<QuestionModel> questions { get; set; }
        public DbSet<TestCount> testCounts { get; set; }
    }
}
