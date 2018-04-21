using JobPortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Data
{
    public class RecruiterDbContext:DbContext
    {
        public RecruiterDbContext(DbContextOptions<RecruiterDbContext> options):base(options)
        {

        }

        public DbSet<RecruiterModel> Recruiters { set; get; }
    }
}
