namespace AwesomeMusic.Services.Utility
{
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public class IdentityUtility : IIdentityUtility
    {
        private readonly IHttpContextAccessor _context;

        public IdentityUtility(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetNameIdentifier()
            => _context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
