using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query;

public class GetAlliillnessReasonsQueryHandler : IRequestHandler<GetAlliillnessReasonsForillnessFormQuery, List<illnessNamesListDTO>>
{
    private readonly IBasicInfoDetailRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAlliillnessReasonsQueryHandler(IBasicInfoDetailRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<List<illnessNamesListDTO>> Handle(GetAlliillnessReasonsForillnessFormQuery request, CancellationToken cancellationToken)
    {
        List<illnessNamesListDTO> result = new();

        Log log = new();

        try
        {
            var illnessreasons = await _repository.GetByBasicInfoIllnessId();

            result = _mapper.Map<List<illnessNamesListDTO>>(illnessreasons);

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

