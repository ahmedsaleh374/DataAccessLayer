using AutoMapper;
using BusinessLogicLayer.Dtos.DepartmentDto;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [IgnoreAntiforgeryToken]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IWebHostEnvironment environment, ILogger<DepartmentController> logger,IMapper mapper)
        {
            _departmentService = departmentService;
            _environment = environment;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(string? DepartmentSearchName)
        {
            var departments = _departmentService.GetAll(DepartmentSearchName);
            return departments is not null? View("Index", departments) : NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto input)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    int result = _departmentService.Create(input);
                    string message;
                    if (result > 0)
                        message = $"Department:{input.Name} is Created Successfully";
                    else
                        message = $"Department:{input.Name} can be created";
                    TempData["message"]= message ;

                    return RedirectToAction(nameof(Index));
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
            try
            {
                if (!id.HasValue)
                    return BadRequest();
                var department = _departmentService.GetById(id.Value);
                if (department is null)
                    return NotFound();

                return View(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentService.GetById(id.Value);

            var deptDto = _mapper.Map<UpdateDepartmentDto>(department);

            #region manual mapping 
            //var deptDto = new UpdateDepartmentDto
            //{
            //    Id = department.Id,
            //    Name = department.Name,
            //    code = department.code,
            //    Description = department.Description,
            //};
            #endregion

            return deptDto is not null ?View(deptDto):NotFound();
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int? id, UpdateDepartmentDto input)
        {
            if (!id.HasValue)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _departmentService.Update(input);
                    string message;
                    if (result > 0)
                        message = $"Department:{input.Name} is updated Successfully";
                    else
                        message = $"Department:{input.Name} can be updated";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                    //return View("Error", ex);
                }
            }
            return View(input);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            try
            {
                int result = _departmentService.Delete(id.Value);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error", ex);
            }

        }
    }
}
