using Microsoft.Extensions.Primitives;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface IQueryStringResolver
    {
        Task<Guid> GetOfficeId();

        Task<Dictionary<string, StringValues>> GetAllQueryStrings();
    }
}