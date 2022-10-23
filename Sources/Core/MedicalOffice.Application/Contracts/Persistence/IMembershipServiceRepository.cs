using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMembershipServiceRepository : IGenericRepository<MemberShipService, Guid>
    {
        Task<MemberShipService> InsertServiceToMemberShipAsync(string discount, Guid serviceId, Guid memberShipId);
    }
}
