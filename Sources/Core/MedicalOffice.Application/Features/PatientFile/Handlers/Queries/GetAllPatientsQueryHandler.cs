using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries;

public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, List<PatientListDto>>
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

    public async Task<List<PatientListDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        List<PatientListDto> result = new();

        Log log = new();

        try
        {
            var patients = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);

            result = _mapper.Map<List<PatientListDto>>(patients);

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
