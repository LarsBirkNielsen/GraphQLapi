using System.Security.Claims;

namespace GraphQL.AuditLibrary.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;


        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;

        }

        public string GetUserId()
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetUserEmail()
        {
            return _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }
    }
}
