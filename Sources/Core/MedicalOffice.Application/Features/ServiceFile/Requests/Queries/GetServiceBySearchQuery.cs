using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Queries;

public class GetServiceBySearchQuery : IRequest<BaseResponse>
{
    public ListDto Dto { get; set; } = new ListDto();
    public Guid OfficeId { get; set; }
    public Guid SectionId { get; set; }
    public string Name { get; set; }
}
