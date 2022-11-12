using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IPermissionRepository : IGenericRepository<Permission, Guid>
    {
        Task<IReadOnlyList<Permission>> GetPermissionDetailsByUserID(Guid Id);

        Task<string> GetId(Guid id);

        //Task<bool> SearchMedicalStaff(Guid searchId);

        Task<List<Permission>> GetByUserAndOfficeId(Guid userId,Guid officeId); 
    }
}
