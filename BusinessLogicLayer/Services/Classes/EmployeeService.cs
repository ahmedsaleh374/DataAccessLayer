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
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService)
        {
            //_employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentService = attachmentService;
        }

        public int Create(CreateEmployeeDto input)
        {
            var employee = _mapper.Map<Employee>(input);

            //_unitOfWork.EmployeeRepository.Create(employee);
            _unitOfWork.EmployeeRepository.Create(employee);
            if (input.Image is not null)
                employee.Image = _attachmentService.Upload(input.Image, "images");

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
                || x.Email.ToLower().Trim().Contains((SearchValue.ToLower().Trim())));

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
            var employee = _mapper.Map<Employee>(input);
            if (input.Image is not null && input.Image.Length > 0)
            {
                var uploadedImage = _attachmentService.Upload(input.Image, "images");
                if (uploadedImage is not null)
                    employee.Image = uploadedImage;
                else
                      employee.Image = input.OldImage;

            }
            //employee.Image = _attachmentService.Upload(input.Image, "images");
            _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.SaveChanges();

            #region GPT SOLUTIONS 
                //if (input.Image is not null)
                //{
                //    var uploadedImage = _attachmentService.Upload(input.Image, "images");
                //    if (uploadedImage is not null)
                //        employee.Image = uploadedImage;
                //    else
                //
                //      employee.Image = input.OldImage;
                //
                //}

            //_unitOfWork.EmployeeRepository.Update(employee);
            //return _unitOfWork.SaveChanges(); 
            #endregion
        }
    }
}
