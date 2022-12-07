using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class AppointmentStatusDto
    {
        public Guid AppointmentId { get; set; }

        public AppointmentType Status { get; set; }
    }
}