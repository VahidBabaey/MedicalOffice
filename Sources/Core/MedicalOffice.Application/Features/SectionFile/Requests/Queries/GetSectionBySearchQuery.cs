using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetSectionBySearchQuery : IRequest<BaseResponse>
{
    public ListDto Dto { get; set; } = new ListDto();
    public string Name { get; set; }
    public Guid OfficeId { get; set; }
    public Order? Order { get; set; }
}