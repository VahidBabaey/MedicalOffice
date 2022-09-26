using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteSectionCommand : IRequest<BaseCommandResponse>
{
    public Guid SectionId { get; set; }
}
