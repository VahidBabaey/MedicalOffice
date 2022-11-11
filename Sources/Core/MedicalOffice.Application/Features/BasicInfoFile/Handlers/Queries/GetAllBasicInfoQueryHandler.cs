using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoFile.Handlers.Queries
{

    public class GetAllBasicInfoQueryHandler : IRequestHandler<GetAllBasicInfoQuery, List<BasicInfoListDTO>>
    {
        private readonly IBasicInfoRepository  _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllBasicInfoQueryHandler(IBasicInfoRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<BasicInfoListDTO>> Handle(GetAllBasicInfoQuery request, CancellationToken cancellationToken)
        {
            List<BasicInfoListDTO> result = new();

            Log log = new();

            try
            {
                var basicInfos = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);
                

                result = _mapper.Map<List<BasicInfoListDTO>>(basicInfos);

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
