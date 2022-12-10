using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands
{
    public class EditAppointmentStatusCommand : IRequest<BaseResponse>
    {
        public AppointmentStatusDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}