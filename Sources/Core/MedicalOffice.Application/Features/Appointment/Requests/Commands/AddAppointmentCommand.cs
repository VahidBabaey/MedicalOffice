using MediatR;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.Appointment.Requests.Commands
{
    public class AddAppointmentCommand : IRequest<BaseResponse>
    {
        public AppointmentDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}