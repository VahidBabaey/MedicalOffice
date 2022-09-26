using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMembershipRepository : IGenericRepository<Membership, Guid>
    {
        Task<Service> DeleteMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
        Task<Service> InsertMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
        Task<Service> UpdateMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
    }
}
