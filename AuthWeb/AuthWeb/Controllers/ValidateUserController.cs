using Microsoft.AspNetCore.Mvc;
using AuthWeb.Areas.Identity.Pages.Account;
using AuthWeb.Services;
using AuthWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Runtime.CompilerServices;


namespace AuthWeb.Controllers
{

    public class ValidateUserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHashingService _hashingService;

        public ValidateUserController(IUserService userService, UserManager<ApplicationUser> userManager, IHashingService hashingService)
        {
            _userService = userService;
            _userManager = userManager;
            _hashingService = hashingService;
        }
        public IActionResult SecurityQuestions(string userName)
        {
            var user = _userService.GetUserByUserName(userName);
            if (user != null)
            {
                ViewData["SecurityQuestion1"] = user.SecurityQn1;
                ViewData["SecurityQuestion2"] = user.SecurityQn2;
                ViewData["SecurityQuestion3"] = user.SecurityQn3;
                return View();
            } 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SecurityQuestions(string a,string userName)
        {
            var user = _userService.GetUserByUserName(userName);
            string securityAns1 = _hashingService.HashInput(Request.Form["SecurityAns1"]);
            string securityAns2 = _hashingService.HashInput(Request.Form["SecurityAns2"]);
            string securityAns3 = _hashingService.HashInput(Request.Form["SecurityAns3"]);
            if (securityAns1 == user.SecurityAns1 &&
                securityAns2 == user.SecurityAns2 &&
                securityAns3 == user.SecurityAns3)
            {
                TempData["success"] = "Answers matched.";
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                return RedirectToAction("ChangePassword",new { UserName = userName, Token = token });
            }
            else
            {
                TempData["faliure"] = "Invalid Answers.";
                return RedirectToAction("SecurityQuestions", new { UserName = userName });
            }
            
        }
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userName, string token)
        {
            var user = _userService.GetUserByUserName(userName);
            string newPassword = Request.Form["newPassword"];
            string confrimPassword = Request.Form["confirmPassword"];
            if (user == null)
            {
                return NotFound();
            }
            if (newPassword == confrimPassword)
            {
                var result = await _userManager.ResetPasswordAsync(user,token,newPassword); 
                if (result.Succeeded)
                {
                    TempData["success"] = "Password changed successfully.";
                    return RedirectToPage("/Account/Login", new { area = "Identity"});
                }
                else
                {
                    TempData["faliure"] = "Password could not be changed successfully.";
                    return View(result);
                }
            }
            else
            {
                TempData["faliure"] = "New password and confirm password do not match.";
                return View();
            }

        }
    }

}
