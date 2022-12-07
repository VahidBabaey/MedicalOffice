using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.Appointment.Requests.Queries
{
    public class SearchByRequestedFieldsQuery : IRequest<BaseResponse>
    {
        public SearchAppointmentsDto DTO { get; set; }
    }
}