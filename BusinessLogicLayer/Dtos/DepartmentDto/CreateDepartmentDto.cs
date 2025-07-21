using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.DepartmentDto
{
    public class CreateDepartmentDto
    {
        public int Id { get; set; }          
        [Required]
        public string Name { get; set; }

        public string? code { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
