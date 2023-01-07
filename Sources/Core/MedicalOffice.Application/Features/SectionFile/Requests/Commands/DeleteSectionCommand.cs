using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteSectionCommand : IRequest<BaseResponse>
{
    public Guid SectionId { get; set; }
    public Guid OfficeId { get; set; }
}
