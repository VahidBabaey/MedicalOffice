using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMembershipRepository : IGenericRepository<Membership, Guid>
    {
        Task<bool> CheckExistMembershipId(Guid officeId, Guid membershipId);
        Task DeleteMembershipIdofServiceAsync(Guid membershipId);
        Task<List<Membership>> GetMembershipBySearch(string name);
        Task<MemberShipService> InsertMembershipIdofServiceAsync(string tariff, Guid serviceId, Guid membershipId);
        Task<string> SearchServicesforMemberShip(Guid memid);
        Task UpdateMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
    }
}
