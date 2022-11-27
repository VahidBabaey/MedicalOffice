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
        if (_context.HttpContext != null)
        {
            return Task.FromResult(_context.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier));
        }

        return Task.FromResult(string.Empty);
    }

    public Task<List<string>> GetUserRoles()
    {
        if (_context.HttpContext != null)
        {
            var roles = Task.FromResult(_context.HttpContext.User.FindAll(ClaimTypes.Role).Select(x=>x.Value).ToList());   
            return roles;   
        }

        return Task.FromResult(new List<string>());
    }
}