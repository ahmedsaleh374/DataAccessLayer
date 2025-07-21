using AutoMapper;
using BusinessLogicLayer.Dtos.DepartmentDto;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.DepartmentModel;
using DataAccessLayer.Models.EmployeeModels;
using DataAccessLayer.Repository.Classes;
using DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public int Create(CreateDepartmentDto input)
        {
            #region  manual mapping
            //var department = new Department()
            //{
            //    Name = input.Name,
            //    code = input.code,
            //    Description = input.Description
            //}; 
            #endregion

            var department = _mapper.Map<Department>(input);
            _unitOfWork.DepartmentRepository.Create(department);
            return _unitOfWork.SaveChanges();
        }

        public int Delete(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department == null)
                return -1;
            department.IsDeleted = true;
            _unitOfWork.DepartmentRepository.Update(department);
            return _unitOfWork.SaveChanges();

        }

        public IEnumerable<DepartmentDto> GetAll(string? SearchValue)
        {
            IEnumerable<Department> departments;
            if (string.IsNullOrWhiteSpace(SearchValue))
                departments = _unitOfWork.DepartmentRepository.GetAll();
            else
                departments = _unitOfWork.DepartmentRepository.GetAll(x => x.Name.ToLower().Trim().Contains((SearchValue).ToLower().Trim()));
            var deptList = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return deptList;

            #region OldWay for manual mapping 
            //var dto = new DepartmentDto();
            //department.Id = dto.Id;
            //department.Name = dto.Name;
            //department.code = dto.code;
            //department.Description = dto.Description; ;
            //department.CreatedAt = dto.CreatedAt;
            //return dto; 

            //return departments.Select(department => new DepartmentDto
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    code = department.code,
            //    Description = department.Description,
            //    CreatedAt = department.CreatedAt,
            //}).ToList();
            #endregion
        }
        public DetailsDepartmentDto GetById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            var deptDto = _mapper.Map<DetailsDepartmentDto>(department);
            return deptDto;

            #region OldWay for manual mapping 
            //var dto = new DetailsDepartmentDto();
            //department.Id = dto.Id;
            //department.Name = dto.Name;
            //department.code = dto.code;
            //department.Description = dto.Description; ;
            //department.CreatedAt = dto.CreatedAt;
            //department.UpdatedAt = dto.UpdatedAt;
            //department.UpdatedBy = dto.UpdatedBy;
            //department.CreatedBy = dto.CreatedBy;   
            //department.DeletedBy = dto.DeletedBy;
            //department.DeletedAt = dto.DeletedAt;
            //return dto; 

            //if (department is null)
            //    return null;
            //return new DetailsDepartmentDto()
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    code = department.code,
            //    Description = department.Description,
            //    CreatedBy = department.CreatedBy,
            //    CreatedAt = department.CreatedAt,
            //    UpdatedBy = department.UpdatedBy,
            //    UpdatedAt = department.UpdatedAt,
            //    DeletedBy = department.DeletedBy,
            //    DeletedAt = department.DeletedAt
            //};
            #endregion
        }

        public int Update(UpdateDepartmentDto input)
        {
            //var department = _departmentRepository.GetById(input.Id);
            //if (department == null)
            //    return 0;
            //var deptDto = _mapper.Map(input, department);
            var deptDto = _mapper.Map<Department>(input);
            _unitOfWork.DepartmentRepository.Update(deptDto);
            #region manual mapping
            //department.Name = input.Name;
            //department.code = input.code;
            //department.Description = input.Description; 
            #endregion

            return _unitOfWork.SaveChanges();
        }
    }
}
