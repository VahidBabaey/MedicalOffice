using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries
{
    public class GetAllMedicalStaffTitleQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
    }
}