using DataAccessLayer.Models.DepartmentModel;
using DataAccessLayer.Models.EmployeeModels;
using DataAccessLayer.Models.Shared;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        //public IDepartmentRepository DepartmentRepository { get; }
        //public IEmployeeRepository EmployeeRepository { get; }
        // public IGenericRepository<BaseEntity> genericRepository { get; }
        public IGenericRepository<Employee> EmployeeRepository { get; }
        public IGenericRepository<Department> DepartmentRepository { get; }
        int SaveChanges();
    }
}
