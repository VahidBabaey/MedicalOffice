using MediatR;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceDurationRepositopry : IGenericRepository<ServiceDuration, Guid>
    {
        Task<bool> CheckStaffHasServiceDuration(Guid? medicalStaffId, Guid serviceId);

        Task<List<ServiceDuration>> GetAllByServiceId(Guid serviceId);

        Task<ServiceDurationDetailsDTO> GetByServiceAndStaffId(Guid? medicalStaffId, Guid? serviceId);

        Task DeleteRange(Guid[] ids);
    }
}