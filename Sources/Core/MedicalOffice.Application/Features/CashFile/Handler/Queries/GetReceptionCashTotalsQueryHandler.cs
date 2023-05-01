using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Features.CashFile.Request.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.Extensions.Hosting;
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
            foreach (var item in cashes)
            {
                if (item.CashCarts.Any())
                {
                    cashTotalReceived.CashCart.CashType = CashType.Cart;
                    foreach (var index in item.CashCarts)
                    {
                        cashTotalReceived.CashCart.Cost += index.Cost;
                    }
                }

                if (item.CashChecks.Any())
                {
                    cashTotalReceived.CashCheck.CashType = CashType.Check;
                    foreach (var index in item.CashChecks)
                    {
                        cashTotalReceived.CashCheck.Cost += index.Cost;
                    }
                }

                if (item.CashPoses.Any())
                {
                    cashTotalReceived.CashPose.CashType = CashType.Pos;
                    foreach (var index in item.CashPoses)
                    {
                        cashTotalReceived.CashPose.Cost += index.Cost;
                    }
                }

                if (item.CashMoneies.Any())
                {
                    cashTotalReceived.CashMoney.CashType = CashType.Money;
                    foreach (var index in item.CashMoneies)
                    {
                        cashTotalReceived.CashMoney.Cost += index.Cost;
                    }
                }
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
