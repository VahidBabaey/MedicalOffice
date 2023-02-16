using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMemberShipServiceRepository : IGenericRepository<MemberShipService, Guid>
    {
        Task<bool> CheckExistMemberShipServiceId(Guid officeId, Guid Id);
        Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShip(Guid officeId, Guid memberShipId);
        Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShipBySearch(Guid officeId, Guid memberShipId, string name);
        Task<Guid> GetMembershipServiceId(Guid serviceId, Guid membershipId);
        Task<Guid> InsertServiceToMemberShipAsync(Guid officeId, string discount, Guid serviceId, Guid memberShipId);
        Task<Guid> UpdateServiceOfMemberShipAsync(string discount, Guid OfficeId, Guid id, Guid serviceId, Guid memberShipId);
    }
}
