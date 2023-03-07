using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Query;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Query;

public class GetAllCommitmentsNamesQueryHandler : IRequestHandler<GetAllCommitmentsNamesFormQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IPatientCommitmentFormRepository _patientcommitmentformrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllCommitmentsNamesQueryHandler(IOfficeRepository officeRepository, IPatientCommitmentFormRepository patientcommitmentformrepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _patientcommitmentformrepository = patientcommitmentformrepository;
        _mapper = mapper;
        _logger = logger;             
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllCommitmentsNamesFormQuery request, CancellationToken cancellationToken)
    {
        var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

        if (!validationOfficeId)
        {
            var error = "OfficeID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }
        try
        {
            var commitmentForms = await _patientcommitmentformrepository.GetFormCommitments(request.OfficeId);

            var result = _mapper.Map<List<CommitmentNamesListDTO>>(commitmentForms);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = commitmentForms.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = commitmentForms.Count(), result = result });
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

