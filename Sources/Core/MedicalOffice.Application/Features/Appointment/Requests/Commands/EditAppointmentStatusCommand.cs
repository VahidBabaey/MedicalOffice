using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.Appointment.Requests.Commands
{
    public class EditAppointmentStatusCommand : IRequest<BaseResponse>
    {
        public AppointmentStatusDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}