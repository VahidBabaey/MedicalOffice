using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.TariffFile.Requests.Queries
{
    public class GetGenericCodeTariffQuery : IRequest<BaseResponse>
    {
        public string GenericCode { get; set; }

        public Guid InsuranceId{ get; set; }

        public Guid OfficeId { get; set; }
    }
}