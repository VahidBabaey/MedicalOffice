using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetAllSectionQuery : IRequest<List<SectionListDTO>>
{
    public ListDto Dto { get; set; } = new ListDto();

    public Guid OfficeId { get; set; }
}
