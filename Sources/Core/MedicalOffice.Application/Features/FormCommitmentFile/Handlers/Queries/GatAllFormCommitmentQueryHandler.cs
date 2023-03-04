using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Queries;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Handlers.Queries
{
    public class GatAllFormCommitmentQueryHandler : IRequestHandler<GatAllFormCommitmentQuery, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IFormCommitmentRepository _formcommitmentrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GatAllFormCommitmentQueryHandler(IOfficeRepository officeRepository, IFormCommitmentRepository formcommitmentrepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _formcommitmentrepository = formcommitmentrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GatAllFormCommitmentQuery request, CancellationToken cancellationToken)
        {

            var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

            if (!validationOfficeId)
            {
                var error = "OfficeID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            try
            {
                var formCommitments = _formcommitmentrepository.GetAll().Result.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false);
                var result = _mapper.Map<List<FormCommitmentListDTO>>(formCommitments.Skip(request.Dto.Skip).Take(request.Dto.Take));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = formCommitments.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = formCommitments.Count(), result = result });
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
