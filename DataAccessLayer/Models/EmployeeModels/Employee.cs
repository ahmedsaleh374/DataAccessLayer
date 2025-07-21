using DataAccessLayer.Models.DepartmentModel;
using DataAccessLayer.Models.EmployeeModels.Enums;
using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.EmployeeModels
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }

        public int? Age { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateTime HireDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public string? Image {  get; set; }

        //relation with department
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

    }
}
