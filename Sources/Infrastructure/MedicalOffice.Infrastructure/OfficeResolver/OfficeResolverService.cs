using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public class OfficeResolverService : IOfficeResolver
{
    private readonly IHttpContextAccessor _context;
    public OfficeResolverService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public Task<Guid> GetOfficeId()
    {
        if (_context.HttpContext != null)
        {
            return Task.FromResult(Guid.Parse(QueryHelpers.ParseQuery(_context.HttpContext.Request.QueryString.Value)

                    .ToDictionary(x => x.Key, x => x.Value)["officeId"]));
        }

        return Task.FromResult(Guid.Empty);
    }
}