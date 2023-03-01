using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IOfficeRepository : IGenericRepository<Office, Guid>
    {
        Task<bool> IsOfficeExist(Guid officeId);
        Task<bool> GetByTelePhoneNumber(string telePhoneNumber);
        Task<List<Office>> GetByUserId(Guid userId);
        Task<bool> isTelePhoneNumberExist(string phone);
    }
}