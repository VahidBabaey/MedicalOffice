using MediatR;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class EditSectionCommand : IRequest<BaseCommandResponse>
{
    public SectionDTO Dto { get; set; } = new SectionDTO();
}
