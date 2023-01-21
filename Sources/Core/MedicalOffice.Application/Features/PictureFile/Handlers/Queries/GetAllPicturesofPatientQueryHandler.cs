using AutoMapper;
using MediatR;
using System.IO;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PictureDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.PictureFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using MedicalOffice.Application.Responses;
using MedicalOffice.Application.Dtos.InsuranceDTO;

namespace MedicalOffice.Application.Features.PictureFile.Handlers.Queries;
public class GetAllPicturesofPatientQueryHandler : IRequestHandler<GetAllPicturesofPatientQuery, BaseResponse>
{
    private readonly IPictureRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllPicturesofPatientQueryHandler(IPictureRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllPicturesofPatientQuery request, CancellationToken cancellationToken)
    {
        Log log = new();

        try
        {
            var pictures = await _repository.GetByPatientId(request.PatientId);

            var result = _mapper.Map<List<PatientPicturesDTO>>(pictures);

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
