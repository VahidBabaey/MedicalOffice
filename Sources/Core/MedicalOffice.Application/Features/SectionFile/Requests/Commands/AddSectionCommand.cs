using MediatR;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class AddSectionCommand : IRequest<BaseResponse>
{ 
    public AddSectionDTO Dto { get; set; } = new AddSectionDTO();
}
