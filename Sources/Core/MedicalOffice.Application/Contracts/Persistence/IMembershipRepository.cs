using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMembershipRepository : IGenericRepository<Membership, Guid>
    {
        Task<bool> CheckExistMembershipId(Guid officeId, Guid membershipId);
        Task<bool> CheckExistMembershipName(Guid officeId, string membershipName);
        Task DeleteMembershipIdofServiceAsync(Guid membershipId);
        Task<List<MembershipsNamesDTO>> GetAllMembershipsNames(Guid officeId);
        Task<List<Membership>> GetMembershipBySearch(string name);
        Task<MemberShipService> InsertMembershipIdofServiceAsync(string tariff, Guid serviceId, Guid membershipId);
        Task<string> SearchServicesforMemberShip(Guid memid);
        Task UpdateMembershipIdofServiceAsync(Guid serviceId, Guid membershipId);
    }
}
