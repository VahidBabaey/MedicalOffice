using MediatR;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.TariffFile.Requests.Queries
{
    public class GetGenericCodeTariffQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public CalculateTariffsReqDTO DTO { get; set; }
    }
}