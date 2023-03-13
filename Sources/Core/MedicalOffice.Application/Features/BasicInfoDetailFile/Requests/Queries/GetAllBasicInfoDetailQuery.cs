using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries
{
    public class GetAllBasicInfoDetailQuery : IRequest<BaseResponse>
    {
        public ListDto DTO { get; set; } = new ListDto();
        public Guid BasicInfoId { get; set; }
        public Guid OfficeId { get; set; }
        public Order? Order { get; set; }
    }
}
