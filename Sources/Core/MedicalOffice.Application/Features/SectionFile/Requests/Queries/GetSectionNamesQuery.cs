using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetSectionNamesQuery : IRequest<List<SectionNamesListDTO>>
{
    public Guid OfficeId { get; set; }
}
