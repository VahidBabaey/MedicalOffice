using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO
{
    public class UpdateAppointmentDescriptionDTO: IAppointmentIdDTO
    {
        public Guid AppointmentId { get; set; }
        public string Description { get; set; }
    }
}