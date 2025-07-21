using DataAccessLayer.Models.EmployeeModels;
using DataAccessLayer.Models.EmployeeModels.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configuration.EmployeeConfig
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(20)");
            builder.Property(e => e.Address).HasColumnType("varchar(100)");

            builder.Property(e => e.Gender)
                .HasConversion((Gender) => Gender.ToString()
                ,(Gender) =>(Gender)Enum.Parse(typeof(Gender),Gender));

            builder.Property(e => e.EmployeeType)
                .HasConversion((EmployeeType) => EmployeeType.ToString()
                , (EmployeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), EmployeeType));

            //filter to use soft dalete
            builder.HasQueryFilter(e=>e.IsDeleted == false);
        }
    }
}
