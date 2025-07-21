using DataAccessLayer.Models.EmployeeModels;
using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DepartmentModel
{
    public class Department: BaseEntity
    {
        public string Name { get; set; }
        public string code { get; set; }
        public string? Description { get; set; }

        //relation with Employee 
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
