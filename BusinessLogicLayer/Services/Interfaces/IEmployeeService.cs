using BusinessLogicLayer.Dtos.DepartmentDto;
using BusinessLogicLayer.Dtos.EmployeeDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IEmployeeService
    {
        int Create(CreateEmployeeDto input);
        int Update(UpdateEmployeeDto input);
        int Delete(int id);
        IEnumerable<EmployeeDto> GetAll(string? SearchValue);
        //IEnumerable<EmployeeDto> GetAll();

        DetailsEmployeeDto? GetById(int id);
    }
}
