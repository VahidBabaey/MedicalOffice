using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Handlers.Queries
{

    public class GetAllBasicInfoDetailQueryHandler : IRequestHandler<GetAllBasicInfoDetailQuery, BaseResponse>
    {
        private readonly IBasicInfoDetailRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllBasicInfoDetailQueryHandler(IBasicInfoDetailRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllBasicInfoDetailQuery request, CancellationToken cancellationToken)
        {
            var log = new Log();

            try
            {
                var basicInfoDetails = await _repository.GetByBasicInfoId(request.BasicInfoId);
                var result = _mapper.Map<List<BasicInfoDetailListDTO>>(basicInfoDetails);

                log.Header = $"{_requestTitle} succeded";
                log.AdditionalData = result;
                log.Type = LogType.Success;
                await _logger.Log(log);

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData = error.Message;
                log.Type = LogType.Error;
                await _logger.Log(log);

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} succeeded", error.Message);
            }
        }
    }
}
