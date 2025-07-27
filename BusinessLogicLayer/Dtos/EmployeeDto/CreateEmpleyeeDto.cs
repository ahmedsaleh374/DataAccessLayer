using DataAccessLayer.Models.EmployeeModels.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.EmployeeDto
{
    public class CreateEmployeeDto
    {
        [Required]
        [StringLength(50,ErrorMessage ="max length should be 50 char")]
        public string Name { get; set; }

        [Range(22,40)]
        public int? Age { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s,.-]{5,100}$", ErrorMessage = "Address must be between 5 and 100 characters and contain only letters, numbers, commas, dots, and dashes.")]
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        [Display(Name ="Phone Number")]
        public string? Phone { get; set; }

        [Display(Name ="Hiring Date")]
        public DateOnly HireDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }

    }
}
