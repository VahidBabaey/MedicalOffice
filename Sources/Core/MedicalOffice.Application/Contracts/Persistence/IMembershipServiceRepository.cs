using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMemberShipServiceRepository : IGenericRepository<MemberShipService, Guid>
    {
        Task<bool> CheckExistMemberShipServiceId(Guid Id);
        Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShip(Guid officeId, Guid memberShipId);
        Task<MemberShipService> InsertServiceToMemberShipAsync(Guid officeId, string discount, Guid serviceId, Guid memberShipId);
        Task<MemberShipService> UpdateServiceOfMemberShipAsync(string discount, Guid serviceId, Guid memberShipId);
    }
}
