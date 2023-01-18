using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.BasicInfoFile.Handlers.Queries
{

    public class GetAllBasicInfoQueryHandler : IRequestHandler<GetAllBasicInfoQuery, BaseResponse>
    {
        private readonly IBasicInfoRepository _repository;
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

        public async Task<BaseResponse> Handle(GetAllBasicInfoQuery request, CancellationToken cancellationToken)
        {
            Log log = new();

            try
            {
                var basicInfos = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);
                var result = _mapper.Map<List<BasicInfoListDTO>>(basicInfos.Where(p => p.OfficeId == request.OfficeId));

                log.Header = $"{_requestTitle} succeded";
                log.AdditionalData = result;
                log.Type = LogType.Success;
                await _logger.Log(log);

                return ResponseBuilder.Success(System.Net.HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData = error.Message;
                log.Type = LogType.Error;
                await _logger.Log(log);

                return ResponseBuilder.Success(System.Net.HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
            }
        }
    }
}
