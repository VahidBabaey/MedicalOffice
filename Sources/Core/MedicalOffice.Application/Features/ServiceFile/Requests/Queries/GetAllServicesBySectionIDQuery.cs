using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Queries;

public class GetAllServicesBySectionIDQuery : IRequest<BaseResponse>
{
    public ListDto Dto { get; set; } = new ListDto();
    public Guid SectionId { get; set; }
    public Guid OfficeId { get; set; }
}
