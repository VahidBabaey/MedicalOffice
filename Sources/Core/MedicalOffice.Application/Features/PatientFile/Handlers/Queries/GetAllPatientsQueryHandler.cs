using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, BaseResponse>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllPatientsQueryHandler(IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        Log log = new();

        try
        {
            var patients = await _repository.GetAllWithPaggination(request.Dto.Skip, request.Dto.Take);
            var result = _mapper.Map<List<PatientListDTO>>(patients);

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;
            log.AdditionalData = result;
            await _logger.Log(log);

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = result.Count(), result = result });
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
