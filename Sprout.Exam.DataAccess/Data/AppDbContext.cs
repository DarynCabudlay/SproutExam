using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Employee> Employee { get; set; }
       
    }
}
