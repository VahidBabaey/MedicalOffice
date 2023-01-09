using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands
{
    public class UpdateAppointmentCommand : IRequest<BaseResponse>
    {
        public TransferAppointmentDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}