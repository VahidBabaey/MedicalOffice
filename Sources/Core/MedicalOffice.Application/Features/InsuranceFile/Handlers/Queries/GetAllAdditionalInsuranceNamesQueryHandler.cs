using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Handlers.Queries
{

    public class GetAllAdditionalInsuranceNamesQueryHandler : IRequestHandler<GetAllAdditionalInsuranceNamesQuery, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IInsuranceRepository _insurancerepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllAdditionalInsuranceNamesQueryHandler(IOfficeRepository officeRepository, IInsuranceRepository insurancerepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _insurancerepository = insurancerepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetAllAdditionalInsuranceNamesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var insuranceNames = await _insurancerepository.GetAllAdditionalInsuranceNames(request.OfficeId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = insuranceNames.Count(), result = insuranceNames }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = insuranceNames.Count(), result = insuranceNames });
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
