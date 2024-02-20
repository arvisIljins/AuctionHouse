using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services
{
    public class CustomProfileServices : IProfileService
    {
        private  UserManager<ApplicationUser> _userManager;

        public CustomProfileServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var existingClimes = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim("username", user.UserName)
            };

            context.IssuedClaims.AddRange(claims);
            context.IssuedClaims.Add(existingClimes.FirstOrDefault(x => x.Type == JwtClaimTypes.Name));
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}