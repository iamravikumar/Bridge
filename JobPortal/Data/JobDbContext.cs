using JobPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Data
{
    public class JobDbContext:DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {

        }

        public DbSet<JobModel> Jobs { set; get; }
    }
}
