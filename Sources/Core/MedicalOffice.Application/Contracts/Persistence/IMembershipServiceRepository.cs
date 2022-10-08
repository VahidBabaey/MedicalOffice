using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMembershipServiceRepository : IGenericRepository<MemberShipService, Guid>
    {
        //Task<Service> DeleteMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
        //Task<Service> InsertMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
        //Task<Service> UpdateMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
    }
}
