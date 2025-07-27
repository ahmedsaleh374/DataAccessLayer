using DataAccessLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using PresentationLayer.ViewModels;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM input)
        {
            if (ModelState.IsValid)
            {
                var registerModel = new ApplicationUser
                {
                    UserName = input.UserName,
                    Email = input.Email,
                    FirstName = input.firstName,
                    LastName = input.LastName,
                };

                var result = await _userManager.CreateAsync(registerModel, input.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(input);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    var isCorrectPassword = await _userManager.CheckPasswordAsync(user, input.Password);
                    if (isCorrectPassword)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, false);
                        if (result.IsNotAllowed)
                            ModelState.AddModelError(string.Empty, " it is not allowed");
                        if (result.IsLockedOut)
                            return RedirectToAction("Index", "it is not locked");

                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "invalid login");
            return View(input);
        }

        [HttpGet]
        public async Task<IActionResult> SignOut() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
