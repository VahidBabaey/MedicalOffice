using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteSectionListCommand : IRequest<BaseResponse>
{
    public SectionListIDDTO DTO { get; set; } = new SectionListIDDTO();
    public Guid OfficeId { get; set; }
}
