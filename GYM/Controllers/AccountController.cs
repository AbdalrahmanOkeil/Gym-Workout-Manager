using GYM.Models;
using GYM.Repository;
using GYM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GYM.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserRepository userRepository;

        public AccountController(UserManager<ApplicationUser>userManager, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userRepository = (UserRepository?)userRepository;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                ApplicationUser appUser = new ApplicationUser();
                appUser.UserName = model.UserName;
                appUser.Email = model.Email;
                appUser.PasswordHash = model.Password;

                //Save to DB
                IdentityResult result = await userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    //Cookie
                    await signInManager.SignInAsync(appUser, isPersistent: false);
                    return RedirectToAction("Login");
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveLogin(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser appUser = await userManager.FindByNameAsync(model.UserName);
                if(appUser!=null)
                {
                    bool found = await userManager.CheckPasswordAsync(appUser, model.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(appUser, model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email Or Password Wrong!");
            }
            return View("Login", model);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveAdminRegister(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Mapping
                ApplicationUser appUser = new ApplicationUser();
                appUser.UserName = model.UserName;
                appUser.Email = model.Email;
                appUser.PasswordHash = model.Password;

                //Save to DB
                IdentityResult result = await userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(appUser, "Admin");
                    //Cookie
                    await signInManager.SignInAsync(appUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", model);
        }

        public IActionResult Profile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = userRepository.GetUserById(userId);

            if (user == null)
                return NotFound();

            var model = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                ProfileImageURL = user.ImageURL,
                JoinedDate=user.JoinDate
            };

            return View(model);
        }
    }
}
