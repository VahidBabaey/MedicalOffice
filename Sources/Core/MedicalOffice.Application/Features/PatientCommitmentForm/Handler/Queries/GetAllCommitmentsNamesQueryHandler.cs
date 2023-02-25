using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query;

public class GetAllCommitmentsNamesQueryHandler : IRequestHandler<GetAllCommitmentsNamesFormQuery, BaseResponse>
{
    private readonly IBasicInfoDetailRepository _basicInfodetailrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllCommitmentsNamesQueryHandler(IBasicInfoDetailRepository basicInfodetailrepository, IMapper mapper, ILogger logger)
    {
        _basicInfodetailrepository = basicInfodetailrepository;
        _mapper = mapper;
        _logger = logger;             
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllCommitmentsNamesFormQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var commitments = await _basicInfodetailrepository.GetByBasicInfoCommitmentId();

            var result = _mapper.Map<List<CommitmentNamesListDTO>>(commitments);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = commitments.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = commitments.Count(), result = result });
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

