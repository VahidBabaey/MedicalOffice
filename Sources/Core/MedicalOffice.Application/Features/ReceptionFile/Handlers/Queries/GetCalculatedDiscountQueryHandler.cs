using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Queries
{
    public class GetCalculatedDiscountQueryHandler : IRequestHandler<GetCalculatedDiscountQuery, BaseResponse>
    {
        private readonly IReceptionRepository _receptionrepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetCalculatedDiscountQueryHandler(IReceptionRepository receptionrepository, ILogger logger)
        {
            _receptionrepository = receptionrepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetCalculatedDiscountQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var detailsofallreceptions = await _receptionrepository.CalculateDiscount(request.OfficeId, request.ServiceId, request.MembershipId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = detailsofallreceptions
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new {result = detailsofallreceptions });
            }

            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error.Message
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
            }
        }

    }
}
