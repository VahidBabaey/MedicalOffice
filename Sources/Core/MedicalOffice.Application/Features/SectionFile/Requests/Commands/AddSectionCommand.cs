using MediatR;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class AddSectionCommand : IRequest<BaseResponse>
{ 
    public AddSectionDTO DTO { get; set; } = new AddSectionDTO();
    public Guid OfficeId { get; set; }
}
