using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.Appointment.Requests.Queries
{
    public class SearchByPatientQuery : IRequest<BaseResponse>
    {
        public SearchByPatientDto DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}