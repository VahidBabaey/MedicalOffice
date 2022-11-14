using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMemberShipServiceRepository : IGenericRepository<MemberShipService, Guid>
    {
        Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShip(Guid memberShipId);
        Task<MemberShipService> InsertServiceToMemberShipAsync(string discount, Guid serviceId, Guid memberShipId);
        Task<MemberShipService> UpdateServiceOfMemberShipAsync(string discount, Guid serviceId, Guid memberShipId);
    }
}
