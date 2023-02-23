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
    private readonly IOfficeRepository _officeRepository;
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllPatientsQueryHandler(IOfficeRepository officeRepository, IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

        if (!validationOfficeId)
        {
            var error = $"OfficeID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = response.Errors
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }

        try
        {
            var patients = await _repository.GetAllPateint(request.OfficeId);
            var result = _mapper.Map<List<PatientListDTO>>(patients.Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = patients.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = patients.Count(), result = result });
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
