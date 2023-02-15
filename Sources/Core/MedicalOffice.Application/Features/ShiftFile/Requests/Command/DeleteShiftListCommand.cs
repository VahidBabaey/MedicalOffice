using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteShiftListCommand : IRequest<BaseResponse>
{
    public ShiftListIDDTO DTO { get; set; } = new ShiftListIDDTO();
    public Guid OfficeId { get; set; }
}
