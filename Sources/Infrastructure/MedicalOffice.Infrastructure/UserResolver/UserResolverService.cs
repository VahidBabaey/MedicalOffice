using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands;
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

    public Task<List<OfficeRole>> GetOfficeRoles()
    {
        if (_context.HttpContext != null)
        {
            var OfficeRoleClaims = _context.HttpContext.User.Claims.Where(c => c.Type == "OfficeRole").ToList();

            var OfficeRoles = new List<OfficeRole>();

            foreach (var item in OfficeRoleClaims)
            {
                OfficeRoles.Add(new OfficeRole
                {
                    OfficeId = item.Value.Split("|")[0] != string.Empty ? Guid.Parse(item.Value.Split("|")[0]) : null,
                    RoleId = Guid.Parse(item.Value.Split("|")[1])
                });
            }

            return Task.FromResult(OfficeRoles);
        }

        return Task.FromResult(new List<OfficeRole>());
    }
}