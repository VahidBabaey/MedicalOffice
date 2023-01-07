namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface IOfficeResolver
    {
        Task<Guid> GetOfficeId();
    }
}