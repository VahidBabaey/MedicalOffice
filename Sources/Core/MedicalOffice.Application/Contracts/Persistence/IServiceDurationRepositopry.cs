using MediatR;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Contracts.Persistence
{
    public interface IServiceDurationRepositopry : IGenericRepository<ServiceDuration, Guid>
    {
        public Task<ServiceDurationDetailsDTO> GetByServiceAndStaffId(Guid? medicalStaffId, Guid? serviceId);
    }
}