using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zaginionki.Data;
using Zaginionki.Models;

namespace Zaginionki.Controllers
{
    public class UserController : Controller
    {
        private readonly ZaginionkiContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(ZaginionkiContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> login([Bind("UserName,Password,Email")]UserViewModel model)
        {
            if(model.UserName == null)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user.UserName == null&&model.UserName.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(model.UserName);
            }
            if (user==null)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else return View(model);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


    }
}