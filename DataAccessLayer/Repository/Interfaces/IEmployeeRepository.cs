using DataAccessLayer.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
