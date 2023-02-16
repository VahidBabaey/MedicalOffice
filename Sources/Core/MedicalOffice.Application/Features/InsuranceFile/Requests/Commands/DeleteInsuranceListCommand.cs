using MediatR;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteInsuranceListCommand : IRequest<BaseResponse>
{
    public InsuranceListIDDTO DTO { get; set; } = new InsuranceListIDDTO();
    public Guid OfficeId { get; set; }
}
