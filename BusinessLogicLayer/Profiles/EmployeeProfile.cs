using AutoMapper;
using BusinessLogicLayer.Dtos.DepartmentDto;
using BusinessLogicLayer.Dtos.EmployeeDto;
using DataAccessLayer.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Profiles
{
    public class EmployeeProfile : Profile
    {
        
        public EmployeeProfile()
        {
            // create
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate.ToDateTime(TimeOnly.MinValue)));

            //get all
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.EmpType, opt => opt.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.EmpGender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));
            // get by id
            CreateMap<Employee, DetailsEmployeeDto>()
                .ForMember(dest => dest.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.HireDate)));
            // update 
            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(d => d.HireDate, o => o.MapFrom(s => s.HireDate.ToDateTime(TimeOnly.MinValue)));

            /// <summary>
            /// do this for ignore manual mapping because the get by id return detail employee  , we want update employee
            /// </summary>
            CreateMap<DetailsEmployeeDto, UpdateEmployeeDto>()
                .ForMember(d =>d.EmployeeType, o =>o.MapFrom(s =>s.EmployeeType))
                .ForMember(d =>d.Gender, o =>o.MapFrom(s =>s.Gender));

        }

    }
}
