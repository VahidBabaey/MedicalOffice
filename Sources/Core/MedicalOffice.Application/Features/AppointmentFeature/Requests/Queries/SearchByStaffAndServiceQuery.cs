using MediatR;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class SearchByStaffAndServiceQuery : IRequest<BaseResponse>
    {
        public SearchAppointmentsDTO DTO { get; set; }

        public Guid OfficeId{ get; set; }
    }
}