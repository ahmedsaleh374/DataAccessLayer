using AutoMapper;
using BusinessLogicLayer.Dtos.DepartmentDto;
using DataAccessLayer.Models.DepartmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<CreateDepartmentDto,Department>();
            CreateMap<UpdateDepartmentDto,Department>();
            CreateMap<Department,DetailsDepartmentDto>();
            CreateMap<Department,DepartmentDto>();
            CreateMap<DetailsDepartmentDto, UpdateDepartmentDto>();
        }
    }
}
