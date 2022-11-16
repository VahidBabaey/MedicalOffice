using MediatR;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class EditSectionCommand : IRequest<BaseResponse>
{
    public UpdateSectionDTO Dto { get; set; } = new UpdateSectionDTO();
}
