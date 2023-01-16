using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IFormCommitmentRepository : IGenericRepository<FormCommitment, Guid>
    {
        Task<bool> CheckExistFormCommitmentId(Guid officeId, Guid formCommitmentId);
    }
}
