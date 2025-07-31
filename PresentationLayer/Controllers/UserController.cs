using DataAccessLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.ViewModels;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<IActionResult> Index(string searchInput)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrWhiteSpace(searchInput))
                users = await _userManager.Users.ToListAsync();
            else
                users = await _userManager.Users
                    .Where(u => u.NormalizedEmail.Trim().Contains(searchInput.Trim().ToUpper())).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var vm = new UpdateUserVM();

            vm.Id = user.Id;
            vm.UserName = user.UserName;


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UpdateUserVM input)
        {
            if (id != input.Id)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    user.UserName = input.UserName;
                    user.NormalizedUserName = input.UserName.Trim().ToUpper();

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("user Updated successfully");
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                        _logger.LogError(item.Description);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                }
            }

            return View(input);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("USER IS DELETED SUCCESSFULLY");
                }
                foreach (var item in result.Errors)
                {
                    _logger.LogError($"error :{item.Description}");
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"error :{ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
