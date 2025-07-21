using DataAccessLayer.Models.DepartmentModel;
using DataAccessLayer.Models.EmployeeModels;
using DataAccessLayer.Models.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext() { }

        // Fixed the typo in 'Optionsbuilder' to 'DbContextOptions<ApplicationDbContext>'
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore<BaseEntity>();
           // modelBuilder.Entity<Employee>().HasQueryFilter(x =>x.IsDeleted == false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
