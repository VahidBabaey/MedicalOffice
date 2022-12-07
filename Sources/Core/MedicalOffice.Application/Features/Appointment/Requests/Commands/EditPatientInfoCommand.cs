using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;

namespace MedicalOffice.Application.Features.Appointment.Requests.Commands
{
    public class EditAppointmentPatientCommand : IRequest<BaseResponse>
    {
        public PatientInfoDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}