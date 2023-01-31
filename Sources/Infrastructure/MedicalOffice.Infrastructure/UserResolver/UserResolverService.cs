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

    public async Task<Guid> GetUserId()
    {
        if (_context.HttpContext != null)
        {
            return Guid.Parse(_context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        return Guid.Empty;
    }

    public async Task<List<Guid>> GetUserRoles()
    {
        if (_context.HttpContext != null)
        {
            var roles = _context.HttpContext.User.FindAll(ClaimTypes.Role).Select(x=>x.Value).Select(x=>Guid.Parse(x)).ToList();   
            return roles;   
        }

        return new List<Guid>();
    }
}