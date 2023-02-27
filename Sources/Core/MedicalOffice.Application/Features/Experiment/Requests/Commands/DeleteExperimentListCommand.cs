using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteExperimentListCommand : IRequest<BaseResponse>
{
    public ExperimentListIDDTO DTO { get; set; } = new ExperimentListIDDTO();
    public Guid OfficeId { get; set; }
}
