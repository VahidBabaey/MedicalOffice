using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class SearchByPatientQuery : IRequest<BaseResponse>
    {
        public SearchByPatientDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}