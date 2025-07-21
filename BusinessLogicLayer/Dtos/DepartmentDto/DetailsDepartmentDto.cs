using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.DepartmentDto
{
    public class DetailsDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? code { get; set; }
        public string? Description { get; set; }

        public int CreatedBy { get; set; } //userID
        public DateTime CreatedAt { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
