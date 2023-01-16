using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Queries
{
    public class GetReceptionDetailsListQueryHandler : IRequestHandler<GetReceptionDetailsListQuery, List<ReceptionDetailListDTO>>
    {
        private readonly IReceptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetReceptionDetailsListQueryHandler(IReceptionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<ReceptionDetailListDTO>> Handle(GetReceptionDetailsListQuery request, CancellationToken cancellationToken)
        {
            List<ReceptionDetailListDTO> result = new();

            Log log = new();

            try
            {
                var receptionDetailList = await _repository.GetReceptionDetailList(request.PatientId);

                result = _mapper.Map<List<ReceptionDetailListDTO>>(receptionDetailList);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData=(error.Message);
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }

    }
}
