using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoListDTO;
using MedicalOffice.Application.Features.BasicInfoFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.BasicInfoFile.Handlers.Queries
{

    public class GetAllBasicInfoQueryHandler : IRequestHandler<GetAllBasicInfoQuery, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IBasicInfoRepository _basicinforepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllBasicInfoQueryHandler(IOfficeRepository officeRepository, IBasicInfoRepository basicinforepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _basicinforepository = basicinforepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllBasicInfoQuery request, CancellationToken cancellationToken)
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
                var basicInfos =  _basicinforepository.GetAll().Result.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false);
                var result = _mapper.Map<List<BasicInfoListDTO>>(basicInfos);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = basicInfos.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = basicInfos.Count(), result = result });
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
