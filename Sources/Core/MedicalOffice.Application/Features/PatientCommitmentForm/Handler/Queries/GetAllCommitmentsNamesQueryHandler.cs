using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.PatientCommitmentsFormDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query;

public class GetAllCommitmentsNamesQueryHandler : IRequestHandler<GetAllCommitmentsNamesFormQuery, BaseResponse>
{
    private readonly IBasicInfoDetailRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllCommitmentsNamesQueryHandler(IBasicInfoDetailRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;             
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllCommitmentsNamesFormQuery request, CancellationToken cancellationToken)
    {
        Log log = new();

        try
        {
            var commitments = await _repository.GetByBasicInfoCommitmentId();

            var result = _mapper.Map<List<CommitmentNamesListDTO>>(commitments);

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

