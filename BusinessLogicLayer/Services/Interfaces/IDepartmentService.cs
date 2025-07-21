using BusinessLogicLayer.Dtos.DepartmentDto;
using DataAccessLayer.Models.DepartmentModel;
using DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IDepartmentService 
    {
        int Create(CreateDepartmentDto input);
        int Update(UpdateDepartmentDto input);
        int Delete(int id);
        IEnumerable<DepartmentDto> GetAll(string? SearchValue);

        DetailsDepartmentDto? GetById(int id);

    }
}
