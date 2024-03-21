
namespace BidsService.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName() => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "";
    }
}