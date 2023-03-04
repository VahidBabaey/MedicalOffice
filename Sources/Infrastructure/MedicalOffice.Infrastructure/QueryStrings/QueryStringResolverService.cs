using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public class QueryStringResolverService : IQueryStringResolver
{
    private readonly IHttpContextAccessor _context;
    public QueryStringResolverService(IHttpContextAccessor context)
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

    public Task<Dictionary<string, StringValues>> GetAllQueryStrings()
    {
        var output = new Dictionary<string, StringValues>();
        if (_context.HttpContext != null)
        {
            output = QueryHelpers.ParseQuery(_context.HttpContext.Request.QueryString.Value)
                         .ToDictionary(x => x.Key, x => x.Value);
        }

        return Task.FromResult(output);
    }
}