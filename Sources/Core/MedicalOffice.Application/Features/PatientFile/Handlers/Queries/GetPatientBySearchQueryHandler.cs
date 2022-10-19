using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries;

public class GetPatientBySearchQueryHandler : IRequestHandler<GetPatientBySearchQuery, List<PatientListDto>>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetPatientBySearchQueryHandler(IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<List<PatientListDto>> Handle(GetPatientBySearchQuery request, CancellationToken cancellationToken)
    {
        Log log = new();

        List<PatientListDto> result = new();
        try
        {
            await _repository.SearchPateint(request.nationalcode, request.phonenumber, request.filenumber, request.fullname);

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
