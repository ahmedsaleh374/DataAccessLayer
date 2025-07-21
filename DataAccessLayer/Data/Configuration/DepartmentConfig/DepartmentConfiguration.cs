using DataAccessLayer.Models.DepartmentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configuration.DepartmentConfig
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
            builder.Property(d => d.code).IsRequired().HasMaxLength(20);

            //filter to use soft dalete
            builder.HasQueryFilter(e => e.IsDeleted == false);
        }
    }
}
