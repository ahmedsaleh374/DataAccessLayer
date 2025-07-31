using DataAccessLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles ="Admin")]
    //[Authorize]
    [AutoValidateAntiforgeryToken]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleController> _logger;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, ILogger<RoleController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public async Task<IActionResult> Details(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEditRoleVM input)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = input.Name
                    //Id = input.Id
                };

                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation("new role is created successfully");
                    return RedirectToAction(nameof(Index));
                }
                foreach (var item in result.Errors)
                {
                    _logger.LogError(item.Description);
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return View(input);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleVM = new CreateEditRoleVM
            {
                Id = role.Id,
                Name = role.Name,
                
            };

            return View(roleVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CreateEditRoleVM input)
        {
            if (id != input.Id)
                return NotFound();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                role.Name = input.Name;
                role.NormalizedName = input.Name.Trim().ToUpper();

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Role Updated successfully");
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    _logger.LogError(error.Description);
                }
            }
            return View(input);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id) 
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (ModelState.IsValid) 
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded) 
                {
                    _logger.LogInformation("role is deleted successfully");
                    //return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError(string.Empty,error.Description);
                    _logger.LogError(error.Description);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public async Task<IActionResult> AddOrRemoveUser(string Id)
        //{
        //    if (Id == null)
        //    {
        //        return BadRequest("Role ID is null");
        //    }

        //    var role = await _roleManager.FindByIdAsync(Id);
        //    if (role == null)
        //    {
        //        return NotFound("Role not found with Id = " + Id);
        //    }



        //    var users = await _userManager.Users.ToListAsync();
        //    if (users == null)
        //    {
        //        return StatusCode(500, "User list is null");
        //    }


        //    var usersInRoleVM = new List<UserInRoleVM>();

        //    foreach (var user in users)
        //    {
        //        var userInRoleVM = new UserInRoleVM
        //        {
        //            Id = user.Id,
        //            UserName = user.UserName
        //        };

        //        if (await _userManager.IsInRoleAsync(user, role.Name))
        //        {
        //            userInRoleVM.isSelected = true;
        //        }
        //        else
        //        {
        //            userInRoleVM.isSelected = false;
        //        }
        //        usersInRoleVM.Add(userInRoleVM);
        //    }
        //    ViewBag.RoleId = role.Id;
        //    return View(usersInRoleVM);
        //}
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return BadRequest("Role ID is null or empty.");
            }
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                return NotFound("Role not found with Id = " + Id);
            }

            if (string.IsNullOrEmpty(role.Name))
            {
                return StatusCode(500, $"Role with Id = {Id} has a null or empty Name.");
            }

            var users = await _userManager.Users.ToListAsync();
            if (users == null)
            {
                return StatusCode(500, "User list is null");
            }

            var usersInRoleVM = new List<UserInRoleVM>();

            foreach (var user in users)
            {
                var userInRoleVM = new UserInRoleVM
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    isSelected = await _userManager.IsInRoleAsync(user, role.Name)
                };

                usersInRoleVM.Add(userInRoleVM);
            }

            ViewBag.RoleId = role.Id;
            return View(usersInRoleVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string Id,List<UserInRoleVM> UsersInRoleVM)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
                return NotFound();
            if (ModelState.IsValid) 
            {
                foreach (var user in UsersInRoleVM) 
                {
                    var appUser = await _userManager.FindByIdAsync(user.Id);

                    if (appUser != null)
                    {
                        if (user.isSelected && !await _userManager.IsInRoleAsync(appUser,role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser,role.Name);
                        }
                        else if(!user.isSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit), new { Id=role.Id});
            }
            ViewBag.RoleId = role.Id;
            return View(UsersInRoleVM);
            
        }

    }
}
