using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries
{
    public class GetAllOfficeDoctorsQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
    }
}