using MediatR;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands
{
    public class AddAppointmentCommand : IRequest<BaseResponse>
    {
        public AddAppointmentDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}