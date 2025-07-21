using DataAccessLayer.Models.EmployeeModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.EmployeeDto
{
    public class DetailsEmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Address { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateOnly HireDate { get; set; }

        public string Gender { get; set; }

        public string EmployeeType { get; set; }

        public int CreatedBy { get; set; }      //userID
        public DateTime CreatedAt { get; set; } // = DateTime.Now;

        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string? Department { get; set; }

        public int? DepartmentId { get; set; }

        public string? Image { get; set; }

    }
}
