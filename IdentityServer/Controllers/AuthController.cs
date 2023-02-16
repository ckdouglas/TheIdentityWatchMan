using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;

        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginView)
        {
            // check  if the model is valid

            var result = await _signInManager.PasswordSignInAsync(loginView.Username, loginView.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(loginView.ReturnUrl);
            }
            else if (result.IsLockedOut)
            {

            }
            return View();
        }


        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel rm)
        {
            // check  if the model is valid
            if (!ModelState.IsValid)
            {
                return View(rm);
            }

            var user = new IdentityUser(rm.Username);
            var result = await _userManager.CreateAsync(user, rm.Password);

            if (result.Succeeded)
            {

                await _signInManager.SignInAsync(user, false);
                return Redirect(rm.ReturnUrl);
            }
            return View();
        }
    }
}

