using System.Security.Claims;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Account.Registration
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class Index : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public Index(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public RegistrationViewModel Input { get; set; }

        [BindProperty]
        public bool RegistrationSuccess { get; set; }
        [BindProperty]
        public bool ErrorsExist { get; set; }
        [BindProperty]
        public IEnumerable<string> Errors { get; set; }

        public IActionResult OnGet(string returnUrl)
        {
            Input = new RegistrationViewModel
            {
                ReturnUrl = returnUrl,
            };

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Input.Button != "Registration") return Redirect("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (!result.Succeeded)
                {
                    ErrorsExist = true;
                    Errors = result.Errors.Select(x => $"{x.Description} ({x.Code})");
                    return Page();

                }
                if (result.Succeeded)
                {
                    await _userManager.AddClaimsAsync(user, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, Input.FullName)
                    });

                    RegistrationSuccess = true;
                }
            }

            return Page();
        }
    }
}