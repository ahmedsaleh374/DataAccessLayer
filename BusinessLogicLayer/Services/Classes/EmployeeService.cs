using AutoMapper;
using BusinessLogicLayer.Dtos.EmployeeDto;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.EmployeeModels;
using DataAccessLayer.Models.Shared;
using DataAccessLayer.Repository.Classes;
using DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Classes
{
    public class EmployeeService : IEmployeeService
    {
        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int Create(CreateEmployeeDto input)
        {
            var employee = _mapper.Map<Employee>(input);

            //_unitOfWork.EmployeeRepository.Create(employee);
            _unitOfWork.EmployeeRepository.Create(employee);
            return _unitOfWork.SaveChanges();
        }

        public int Delete(int id)
        {
            //var employee = _unitOfWork.EmployeeRepository.GetById(id);
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null)
                return -1;
            //using soft deleting
            employee.IsDeleted = true;
            //_unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.SaveChanges();
        }

        public IEnumerable<EmployeeDto> GetAll(string? SearchValue)
        {
            //IEnumerable<Employee> employees;
            IEnumerable<BaseEntity> employees;
            if (string.IsNullOrEmpty(SearchValue))
                //employees = _unitOfWork.EmployeeRepository.GetAll();
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetAll(x => x.Name.ToLower().Trim().Contains((SearchValue.ToLower().Trim())) 
                || x.Address.ToLower().Trim().Contains((SearchValue.ToLower().Trim()))
                || x.Email.ToLower().Trim().Contains((SearchValue.ToLower().Trim())) );

            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public DetailsEmployeeDto? GetById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return _mapper.Map<DetailsEmployeeDto>(employee);

        }

        public int Update(UpdateEmployeeDto input)
        {
            //var employee = _employeeRepository.GetById(input.Id);
            //if (employee == null)
            //    return 0;
            var empDto = _mapper.Map<Employee>(input);
            _unitOfWork.EmployeeRepository.Update(empDto);
            return _unitOfWork.SaveChanges();
        }
    }
}
