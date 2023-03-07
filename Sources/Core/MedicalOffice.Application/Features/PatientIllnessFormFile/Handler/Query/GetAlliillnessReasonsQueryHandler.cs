using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Features.PatientReferralFormFile.Request.Query;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query;

public class GetAlliillnessReasonsQueryHandler : IRequestHandler<GetAlliillnessReasonsForillnessFormQuery, BaseResponse>
{
    private readonly IBasicInfoDetailRepository _basicinfodetailrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAlliillnessReasonsQueryHandler(IBasicInfoDetailRepository basicinfodetailrepository, IMapper mapper, ILogger logger)
    {
        _basicinfodetailrepository = basicinfodetailrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAlliillnessReasonsForillnessFormQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var illnessreasons = await _basicinfodetailrepository.GetByBasicInfoIllnessId();

            var result = _mapper.Map<List<illnessNamesListDTO>>(illnessreasons);


            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = illnessreasons.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = illnessreasons.Count(), result = result });
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

