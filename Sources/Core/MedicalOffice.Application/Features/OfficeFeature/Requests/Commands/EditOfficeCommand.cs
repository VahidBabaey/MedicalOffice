using MediatR;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;

namespace MedicalOffice.Application.Features.OfficeFeature.Requests.Commands
{
    public class EditOfficeCommand : IRequest<BaseResponse>
    {
        public OfficeDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}