using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.CashFile.Request.Queries
{
    public class GetReceptionCashTotalsQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId;
        public Guid ReceptionId { get; set; }
    }
}