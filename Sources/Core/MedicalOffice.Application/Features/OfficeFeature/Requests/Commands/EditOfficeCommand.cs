using MediatR;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;

namespace MedicalOffice.Application.Features.OfficeFeature.Requests.Commands
{
    public class EditOfficeCommand : IRequest<BaseResponse>
    {
        public UpdateOfficeDTO DTO { get; set; }
    }
}