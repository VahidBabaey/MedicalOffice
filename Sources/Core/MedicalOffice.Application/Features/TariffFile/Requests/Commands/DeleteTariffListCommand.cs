using MediatR;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Commands;

public class DeleteTariffListCommand : IRequest<BaseResponse>
{
    public TariffListIdDTO DTO { get; set; }
    public Guid OfficeId { get; set; }
}
