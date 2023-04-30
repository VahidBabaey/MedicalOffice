using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Features.CashFile.Request.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Handler.Queries
{
    public class GetReceptionCashTotalsQueryHandler : IRequestHandler<GetReceptionCashTotalsQuery, BaseResponse>
    {
        private readonly ICashRepository _cashRepository;
        private readonly string _requestTitle;
        private readonly ILogger _logger;

        public GetReceptionCashTotalsQueryHandler(ICashRepository cashRepository, ILogger logger)
        {
            _logger = logger;
            _cashRepository = cashRepository;
            _requestTitle = GetType().Name.Replace("QueryHAndler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetReceptionCashTotalsQuery request, CancellationToken cancellationToken)
        {
            var cashes = await _cashRepository.GetTotalRecievedByReceptionId(request.OfficeId, request.ReceptionId);

            var cashTotalReceived = new CashTotalReceivedDto();
            foreach (var item in cashes.CashCarts)
            {
                cashTotalReceived.CashCart.CashType = item.CashType;
                cashTotalReceived.CashCart.Cost = +item.Cost;
            }
            foreach (var item in cashes.CashChecks)
            {
                cashTotalReceived.CashCheck.CashType = item.CashType;
                cashTotalReceived.CashCheck.Cost = +item.Cost;
            }
            foreach (var item in cashes.CashPoses)
            {
                cashTotalReceived.CashPose.CashType = item.CashType;
                cashTotalReceived.CashPose.Cost = +item.Cost;
            }
            foreach (var item in cashes.CashMoneies)
            {
                cashTotalReceived.CashMoney.CashType = item.CashType;
                cashTotalReceived.CashMoney.Cost = +item.Cost;
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = cashTotalReceived
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", cashTotalReceived);
        }
    }
}
