using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Section;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetAllSectionQuery : IRequest<List<SectionListDTO>>
{
    public ListDto Dto { get; set; } = new ListDto();
}
