using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IPatientRepository _patientrepository;
    private readonly IPatientContactRepository _contactrepository;
    private readonly IPatientAddressRepository _addressrepository;
    private readonly IPatientTagRepository _tagrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeletePatientCommandHandler(IOfficeRepository officeRepository, IPatientContactRepository contactrepository, IPatientAddressRepository addressrepository, IPatientTagRepository tagrepository, IPatientRepository patientrepository, ILogger logger)
    {
        _officeRepository = officeRepository;
        _patientrepository = patientrepository;
        _contactrepository = contactrepository;
        _addressrepository = addressrepository;
        _tagrepository = tagrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
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
            await _patientrepository.SoftDelete(request.PatientId);
            await _contactrepository.RemovePatientContact(request.PatientId);
            await _addressrepository.RemovePatientAddress(request.PatientId);
            await _tagrepository.RemovePatientTag(request.PatientId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded");
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
