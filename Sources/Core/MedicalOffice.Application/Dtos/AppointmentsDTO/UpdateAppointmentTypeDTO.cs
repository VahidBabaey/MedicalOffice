using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class UpdateAppointmentTypeDTO : IAppointmentIdDTO
    {
        public Guid AppointmentId { get; set; }

        public AppointmentType AppointmentType { get; set; }
    }
}