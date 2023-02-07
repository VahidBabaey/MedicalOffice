using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetInsuranceBySearchQuery : IRequest<BaseResponse>
{
    public ListDto Dto { get; set; } = new ListDto();
    public string Name { get; set; }
    public Guid OfficeId { get; set; }
}
