using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Patient;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries;

public class GetPatientByPhoneNumberQueryHandler : IRequestHandler<GetPatientByPhoneNumberQuery, List<PatientListDto>>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetPatientByPhoneNumberQueryHandler(IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<List<PatientListDto>> Handle(GetPatientByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        List<PatientListDto> result = new();
        Log log = new();


        try
        {
            var patient = await _repository.GetPatientsByPhoneNumber(request.PhoneNumber);

            result = _mapper.Map<List<PatientListDto>>(patient);

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            log.Header = $"{_requestTitle} failed";
            log.Messages.Add(error.Message);
            log.Type = LogType.Error;
        }

        await _logger.Log(log);

        return result;
    }
}
