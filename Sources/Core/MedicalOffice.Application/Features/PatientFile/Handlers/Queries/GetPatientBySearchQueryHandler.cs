using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries;

public class GetPatientBySearchQueryHandler : IRequestHandler<GetPatientBySearchQuery, BaseResponse>
{
    private readonly IPatientRepository _patientrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetPatientBySearchQueryHandler(IPatientRepository patientrepository, IMapper mapper, ILogger logger)
    {
        _patientrepository = patientrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetPatientBySearchQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var pateint = await _patientrepository.SearchPateint(request.OfficeId, request.searchFields.FirstName, request.searchFields.LastName, request.searchFields.NationalID, request.searchFields.Mobile, request.searchFields.FileNumber);
            var result = _mapper.Map<List<PatientListDTO>>(pateint.Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = pateint.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = pateint.Count(), result = result });
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
