using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query;

public class GetAllCommitmentsNamesQueryHandler : IRequestHandler<GetAllCommitmentsNamesFormQuery, List<CommitmentNamesListDTO>>
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

    public async Task<List<CommitmentNamesListDTO>> Handle(GetAllCommitmentsNamesFormQuery request, CancellationToken cancellationToken)
    {
        List<CommitmentNamesListDTO> result = new();

        Log log = new();

        try
        {
            var commitments = await _repository.GetByBasicInfoCommitmentId();

            result = _mapper.Map<List<CommitmentNamesListDTO>>(commitments);

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            log.Header = $"{_requestTitle} failed";
            log.AdditionalData=(error.Message);
            log.Type = LogType.Error;
        }

        await _logger.Log(log);

        return result;
    }
}

