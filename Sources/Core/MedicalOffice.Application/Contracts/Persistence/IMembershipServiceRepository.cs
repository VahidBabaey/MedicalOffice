using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMemberShipServiceRepository : IGenericRepository<MemberShipService, Guid>
    {
        Task<bool> CheckExistMemberShipServiceId(Guid officeId, Guid Id);
        Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShip(int skip, int take, Guid officeId, Guid memberShipId);
        Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShipBySearch(int skip, int take, Guid officeId, Guid memberShipId, string name);
        Task<Guid> GetMembershipServiceId(Guid serviceId, Guid membershipId);
        Task<MemberShipService> InsertServiceToMemberShipAsync(Guid officeId, string discount, Guid serviceId, Guid memberShipId);
        Task<MemberShipService> UpdateServiceOfMemberShipAsync(string discount, Guid OfficeId, Guid id, Guid serviceId, Guid memberShipId);
    }
}
