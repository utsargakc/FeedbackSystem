// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuthWeb.Areas.Identity.Pages.Account;


namespace AuthWeb.Areas.Identity.Pages.Account
{
    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        [BindProperty(SupportsGet = true)]
        public string UserName { get; set; }
        public void OnGet(string userName)
        {
            UserName = userName;
            var registerModel = PageContext.HttpContext.Items[nameof(RegisterModel)] as RegisterModel;

            if (registerModel != null && registerModel.Input.UserName == userName)
            {
                ViewData["SecurityQuestion1"] = registerModel.Input.SecurityQn1;
                ViewData["SecurityQuestion2"] = registerModel.Input.SecurityQn2;
                ViewData["SecurityQuestion3"] = registerModel.Input.SecurityQn3;
            }

        }
        public async Task<IActionResult> OnPostAsync(string userName)
        {
            UserName = userName;
            return RedirectToPage("./ChangePassword");
        }
    }
}
