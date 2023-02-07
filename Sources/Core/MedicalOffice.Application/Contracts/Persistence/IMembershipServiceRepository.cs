using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IMemberShipServiceRepository : IGenericRepository<MemberShipService, Guid>
    {
        Task<bool> CheckExistMemberShipServiceId(Guid Id);
        Task<List<ServicesOfMemeberShipListDTO>> GetAllServicesOfMemberShip(int skip, int take, Guid officeId, Guid memberShipId);
        Task<MemberShipService> InsertServiceToMemberShipAsync(Guid officeId, string discount, Guid serviceId, Guid memberShipId);
        Task<MemberShipService> UpdateServiceOfMemberShipAsync(string discount, Guid OfficeId, Guid id, Guid serviceId, Guid memberShipId);
    }
}
