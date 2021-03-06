﻿using System.Net.Mime;
using SoftUniJobPlatform.Data;
using SoftUniJobPlatform.Data.Models.Enum;
using SoftUniJobPlatform.Web.Infrastructure;

namespace SoftUniJobPlatform.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Security.Policy;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using SoftUniJobPlatform.Data.Models;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ApplicationDbContext dbContext;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<ApplicationRole> roleManager,
            ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.roleManager = roleManager;
            this.dbContext = dbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [StringLength(20, ErrorMessage = " {0} трябва да е поне {2} символа и не повече от {1} символа .", MinimumLength = 4)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Паролата трябва да е поне {2} символа и не повече от {1} символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = "Моля въведете роля.")]
            [Display(Name = "User type")]
            public UserType IsEmployer { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Username, Email = Input.Email, Type = Input.IsEmployer };
                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    var role = await roleManager.FindByNameAsync(Input.IsEmployer.ToString());

                    dbContext.UserRoles.Add(new IdentityUserRole<string>
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    });

                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        var roleString = Input.IsEmployer.ToString();
                        if (roleString == "Employer")
                        {
                            return this.Redirect("/Employer/Dashboard/");
                        }
                        else
                        {
                            return this.Redirect("/Student/Dashboard/");
                        }
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
