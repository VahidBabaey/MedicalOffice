using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetAllSectionQuery : IRequest<BaseResponse>
{
    public ListDto Dto { get; set; } = new ListDto();
    public Guid OfficeId { get; set; }
}
