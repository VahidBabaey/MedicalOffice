using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class GetAllServiceDurationQuery : IRequest<BaseResponse>
    {
        public ListDto DTO { get; set; }
        public Guid OfficeId{ get; set; }
    }
}