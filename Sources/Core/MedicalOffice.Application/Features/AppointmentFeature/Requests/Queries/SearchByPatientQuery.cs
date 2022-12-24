using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class SearchByPatientQuery : IRequest<BaseResponse>
    {
        public string Input{ get; set; }
        public DateTime? Date{ get; set; }
        public Guid OfficeId { get; set; }
    }
}