using JobPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Data
{
    public class JobSeekerDbContext:DbContext
    {

        public JobSeekerDbContext(DbContextOptions<JobSeekerDbContext> options) : base(options)
        {

        }

        public DbSet<JobSeekerModel> JobSeekers { set; get; }
    }
}
