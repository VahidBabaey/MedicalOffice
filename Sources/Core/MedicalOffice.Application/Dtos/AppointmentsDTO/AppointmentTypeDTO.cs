using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class AppointmentTypeDTO
    {
        public Guid AppointmentId { get; set; }

        public AppointmentType AppointmentType { get; set; }
    }
}