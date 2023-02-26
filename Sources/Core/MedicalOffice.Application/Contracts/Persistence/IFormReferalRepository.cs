using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IFormReferalRepository : IGenericRepository<FormReferal, Guid>
    {
        Task<bool> CheckExistFormReferalId(Guid officeId, Guid formReferalId);
        Task<bool> CheckExistFormReferalName(Guid officeId, string formReferalName);
    }
}
