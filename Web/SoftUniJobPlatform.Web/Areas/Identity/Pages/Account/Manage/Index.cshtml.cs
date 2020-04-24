using SoftUniJobPlatform.Common;

namespace SoftUniJobPlatform.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using SoftUniJobPlatform.Data.Models;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
            [Display(Name = "Company Name")]
            public string CompanyName { get; set; }

            [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long or Incorrect format.(example: Ivan Ivanov Ivanov)", MinimumLength = 4)]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "BirthDate")]
            public string BirthDate { get; set; }

            [Display(Name = "ImageUrl")]
            public string ImageUrl { get; set; }

            [Display(Name = "Location")]
            public string Location { get; set; }

            [StringLength(9, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 9)]
            [Display(Name = "Register Number")]
            public string RegisterNumber { get; set; }

            [StringLength(9, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 9)]
            [Display(Name = "Student Number")]
            public string StudentNumber { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Gender")]
            public GenderType Gender { get; set; }

            public string Description { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);

            if (!setPhoneResult.Succeeded)
            {
                var userId = await this.userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
            }
            else if (this.User.IsInRole(GlobalConstants.EmployerRoleName))
            {

                user.FullName = this.Input.CompanyName == null ? user.FullName : this.Input.CompanyName;
                user.RegistrationNumber = this.Input.RegisterNumber == null
                    ? user.RegistrationNumber
                    : this.Input.RegisterNumber;
                user.ImageUrl = this.Input.ImageUrl == null ? user.ImageUrl : this.Input.ImageUrl;
                user.Location = this.Input.Location == null ? user.Location : this.Input.Location;
                user.Description = this.Input.Description == null ? "No Description" : this.Input.Description;
            }
            else if (this.User.IsInRole(GlobalConstants.StudentRoleName))
            {

                user.FullName = this.Input.FullName;
                user.RegistrationNumber = this.Input.RegisterNumber;
                user.ImageUrl = this.Input.ImageUrl;
                user.DateOfBirth = DateTime.Parse(this.Input.BirthDate);
                user.Location = this.Input.Location;
            }
            await this.userManager.UpdateAsync(user);

            await this.signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
