using DataAccessLayer.Data.Context;
using DataAccessLayer.Models.DepartmentModel;
using DataAccessLayer.Models.EmployeeModels;
using DataAccessLayer.Models.Shared;
using DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly Lazy<IDepartmentRepository> _DepartmentRepository;
       // private readonly Lazy<IEmployeeRepository> _EmployeeRepository;
        private readonly ApplicationDbContext _context;
        private readonly Lazy<IGenericRepository<Employee>> _EmployeeRepository;
        private readonly Lazy<IGenericRepository<Department>> _DepartmentRepository;
        

        public UnitOfWork(ApplicationDbContext context)
        {
            //_DepartmentRepository = new Lazy<IDepartmentRepository>( new DepartmentRepository(context));
            //_EmployeeRepository = new Lazy<IEmployeeRepository>( new EmployeeRepository(context));
            _context = context;
            _EmployeeRepository = new Lazy<IGenericRepository<Employee>>( new GenericRepository<Employee>(context));
            _DepartmentRepository = new Lazy<IGenericRepository<Department>>( new GenericRepository<Department>(context));
        }
        //public IDepartmentRepository DepartmentRepository =>  _DepartmentRepository.Value;
        //public IEmployeeRepository EmployeeRepository => _EmployeeRepository.Value;

        public IGenericRepository<Employee> EmployeeRepository => _EmployeeRepository.Value;
        public IGenericRepository<Department> DepartmentRepository => _DepartmentRepository.Value;

        public int SaveChanges() => _context.SaveChanges();
        
    }
}
