using Microsoft.Extensions.Primitives;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface IContextResolver
    {
        Task<Guid> GetOfficeId();

        Task<Dictionary<string, StringValues>> GetAllQueryStrings();
    }
}