using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands
{
    public class UpdateMedicalStaffCommand : IRequest<BaseResponse>
    {
        public MedicalStaffPermissionsDTO DTO { get; set; } = new MedicalStaffPermissionsDTO();

        public Guid OffceId { get; set; }
    }
}
