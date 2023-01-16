using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Queries;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Handlers.Queries
{
    public class GatAllFormCommitmentQueryHandler : IRequestHandler<GatAllFormCommitmentQuery, List<FormCommitmentListDTO>>
    {
        private readonly IFormCommitmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GatAllFormCommitmentQueryHandler(IFormCommitmentRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<FormCommitmentListDTO>> Handle(GatAllFormCommitmentQuery request, CancellationToken cancellationToken)
        {
            List<FormCommitmentListDTO> result = new();

            Log log = new();

            try
            {
                var formCommitments = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);

                result = _mapper.Map<List<FormCommitmentListDTO>>(formCommitments.Where(p => p.OfficeId == request.OfficeId));

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData=error.Message;
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }
}
