using MediatR;
using MedicalOffice.Application.Dtos.DrugDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteDrugListCommand : IRequest<BaseResponse>
{
    public DrugListIDDTO DTO { get; set; } = new DrugListIDDTO();
    public Guid OfficeId { get; set; }
}
