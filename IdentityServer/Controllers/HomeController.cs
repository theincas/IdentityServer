using IdentityServer.Extensions;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Diagnostics;

namespace IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                // Handle the case where the user already exists
                return BadRequest("Email is already taken");
            }


            var identityResult = await _userManager.CreateAsync(new()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.Phone,
            }, request.PasswordConfirm);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "�yelik Kay�t i�lemi ba�ar�l�";
                return Redirect(nameof(HomeController.SignUp));
            }

            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());

            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(model.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya �ifre yanl��");
                return View();
            }

            //4. true olursa kullan�c� cok fazla yanl�s giri� yaparsa kullan�c�y� kitler
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>
                {
                    "�ok fazla denemeden dolay� 3 dakika boyunca hesab�n�z kitlenmi�tir."
                });
                return View();
            }



            ModelState.AddModelErrorList(new List<string>
            {
                "Email veya �ifre yanl��",
                $"Ba�ar�s�z giri� say�s�={await _userManager.GetAccessFailedCountAsync(hasUser)}"
            });


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser==null)
            {
                ModelState.AddModelErrorList(new List<string>
                {
                    "E-Mail adresine sahip kullan�c� bulunamam��t�r."
                });
                return View();
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, Token = passwordResetToken },HttpContext.Request.Scheme);

            await _emailService.SendResetPasswordEmail(passwordResetLink!,hasUser.Email!);

            TempData["SuccessMessage"] = "�ifre yenileme linki e-posta adresinize g�nderilmi�tir.";
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult ResetPassword(string userId,string token)
        {
            TempData["token"] = token;
            TempData["userId"] = userId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];

            if (userId==null || token==null)
            {
                throw new Exception("Bir Hata Meydan Gelmi�tir.");
            }

            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);

            if (hasUser==null)
            {
                ModelState.AddModelError(string.Empty,"Bir Hata Meydana Gelmi�tir.");

                return View();
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "�ifreniz Ba�ar�yla Yenilenmi�tir";
            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
                return View();
            }

            return RedirectToAction(nameof(SignIn));
        }
    }
}
