using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.WebApi.WebApi.Controllers;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class GetSpecificPeriodAppointmentsQuery : IRequest<BaseResponse>
    {
        public GetSpecificPeriodAppointmentDTO DTO { get; set; }

        public Guid OfficeId { get; set; }
    }
}