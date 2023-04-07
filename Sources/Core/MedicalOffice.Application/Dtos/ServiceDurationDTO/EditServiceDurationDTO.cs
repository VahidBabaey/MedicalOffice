
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.ServiceDurationDTO
{
    public class EditServiceDurationDTO : BaseDto<Guid>, IMedicalStaffDTO, IServiceIdDTO
    {
        public Guid MedicalStaffId { get; set; }

        public Guid ServiceId { get; set; }

        public int Duration { get; set; }
    }
}