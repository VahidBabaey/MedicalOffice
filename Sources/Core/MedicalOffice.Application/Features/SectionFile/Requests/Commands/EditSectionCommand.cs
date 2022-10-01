using MediatR;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class EditSectionCommand : IRequest<BaseCommandResponse>
{
    public UpdateSectionDTO Dto { get; set; } = new UpdateSectionDTO();
}
