using AutoMapper;
using BusinessLogicLayer.Dtos.EmployeeDto;
using BusinessLogicLayer.Services.Classes;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models.EmployeeModels.Enums;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    //[ValidateAntiForgeryToken]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment environment, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeService = employeeService;
            _environment = environment;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string? EmployeeSearchName)
        {
                var employees = _employeeService.GetAll(EmployeeSearchName);
                return employees is not null ? View("Index", employees) : NotFound();  
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto input)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeeService.Create(input);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Can not be created");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(input);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var employee = _employeeService.GetById(id.Value);

            return employee is not null ? View(employee) : NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var employee = _employeeService.GetById(id.Value);

            #region manual mapping 
            //var empDto = new UpdateEmployeeDto
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Age = employee.Age,
            //    Salary = employee.Salary,
            //    IsActive = employee.IsActive,
            //    Email = employee.Email,
            //    DepartmentId = employee.DepartmentId,
            //    Department = employee.Department,
            //    HireDate = employee.HireDate,
            //    Phone = employee.Phone,
            //    Gender = Enum.Parse<Gender>(employee.Gender),
            //    EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            //};
            #endregion

            var empDto =_mapper.Map<UpdateEmployeeDto>(employee);
            return employee is not null ? View("Edit", empDto) : NotFound();
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdateEmployeeDto Input)
        {
            if (!id.HasValue)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    int employee = _employeeService.Update(Input);
                    if (employee > 0)
                        return RedirectToAction("Index");
                    else
                        ModelState.AddModelError(string.Empty, "Can not be Updated");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View("Edit", Input);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            try
            {
                int result = _employeeService.Delete(id.Value);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError(string.Empty, "can not be delete");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    ModelState.AddModelError($"{ex.Message}", ex.Message);
                else
                    _logger.LogError(ex.Message);
                return View("Error", ex);
            }

        }
    }
}
