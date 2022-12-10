using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class SearchByRequestedFieldsQuery : IRequest<BaseResponse>
    {
        public SearchAppointmentsDTO DTO { get; set; }
    }
}