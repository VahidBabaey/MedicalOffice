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
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Queries
{
    public class GetDetailsOfAllReceptionsQueryHandler : IRequestHandler<GetDetailsOfAllReceptionsQuery, BaseResponse>
    {
        private readonly IReceptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetDetailsOfAllReceptionsQueryHandler(IReceptionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetDetailsOfAllReceptionsQuery request, CancellationToken cancellationToken)
        {
            DetailsofAllReceptionsDTO result = new();

            Log log = new();

            try
            {
                var detailsofallreceptions = await _repository.GetDetailsofAllReceptions(request.PatientId);

                result = _mapper.Map<DetailsofAllReceptionsDTO>(detailsofallreceptions);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
                log.AdditionalData = result;
                await _logger.Log(log);

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
            }

            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData = error.Message;
                log.Type = LogType.Error;
                await _logger.Log(log);

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
            }
        }

    }
}
