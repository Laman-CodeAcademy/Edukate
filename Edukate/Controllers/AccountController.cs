using Edukate.Models;
using Edukate.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Edukate.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManger) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

      

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "password or email is wrong");
                return View(vm);
            }

            var signInResult = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (!signInResult)
            {
                ModelState.AddModelError("", "password or email is wrong");
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AppUser user = new()
            {
                UserName=vm.UserName,
                Fullname=vm.Fullname,
                Email=vm.Email,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }

            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

       

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManger.CreateAsync(new() { Name = "Admin"});
            await _roleManger.CreateAsync(new() { Name = "Member"});
            await _roleManger.CreateAsync(new() { Name = "Moderator"});
            await _roleManger.CreateAsync(new() { Name = "Admin" });
        return Ok("roles created");
        }

    }
}
