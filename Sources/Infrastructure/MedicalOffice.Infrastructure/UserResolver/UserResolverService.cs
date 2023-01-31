using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public class UserResolverService : IUserResolverService
{
    private readonly IHttpContextAccessor _context;

    public UserResolverService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public async Task<string> GetUserId()
    {
        if (_context.HttpContext != null)
        {
            return _context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        return string.Empty;
    }

    public async Task<List<string>> GetUserRoles()
    {
        if (_context.HttpContext != null)
        {
            var roleNames = _context.HttpContext.User.FindAll(ClaimTypes.Role).Select(x=>x.Value).ToList();
            return roleNames;   
        }

        return new List<string>();
    }
}