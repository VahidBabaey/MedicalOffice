using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.WebApi.WebApi.Controllers;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class GetSpecificDateAppointmentsQuery : IRequest<BaseResponse>
    {
        public SpecificDateAppointmentDTO DTO { get; set; }

        public Guid OfficeId { get; set; }
    }
}