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

namespace MedicalOffice.Application.Features.PictureFile.Handlers.Queries;
public class GetAllPicturesofPatientQueryHandler : IRequestHandler<GetAllPicturesofPatientQuery, BaseResponse>
{
    private readonly IPatientRepository _patientrepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IPictureRepository _picturerepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllPicturesofPatientQueryHandler(IPatientRepository patientrepository, IOfficeRepository officeRepository, IPictureRepository picturerepository, IMapper mapper, ILogger logger)
    {
        _patientrepository = patientrepository;
        _officeRepository = officeRepository;
        _picturerepository = picturerepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllPicturesofPatientQuery request, CancellationToken cancellationToken)
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

        var validationPatientId = await _patientrepository.CheckExistPatientId(request.OfficeId, request.PatientId);

        if (!validationPatientId)
        {
            var error = "ID isn't exist";
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
            var pictures = await _picturerepository.GetByPatientId(request.PatientId);

            var result = _mapper.Map<List<PatientPicturesDTO>>(pictures.Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = pictures.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = pictures.Count(), result = result });
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
