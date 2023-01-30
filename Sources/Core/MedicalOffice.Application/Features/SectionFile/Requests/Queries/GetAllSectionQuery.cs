using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetAllSectionQuery : IRequest<List<SectionListDTO>>
{
    public Guid OfficeId { get; set; }
}
