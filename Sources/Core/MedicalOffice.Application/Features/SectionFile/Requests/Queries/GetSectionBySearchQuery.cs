using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetSectionBySearchQuery : IRequest<List<SectionListDTO>>
{
    public string Name { get; set; }
    public Guid OfficeId { get; set; }
}
