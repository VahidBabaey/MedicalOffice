using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public class UserResolverService : IUserResolverService
{
    private readonly IHttpContextAccessor _context;
    public UserResolverService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public Task<string> GetUserId()
    {
        return Task.FromResult(_context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}

//public static class HttpContextExtentions
//{
//    public static string GetUserId(this HttpContext httpContext)
//    {
//        return httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//    }
//}