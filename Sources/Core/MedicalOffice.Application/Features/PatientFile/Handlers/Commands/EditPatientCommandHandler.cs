using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class EditPatientCommandHandler : IRequestHandler<EditPatientCommand, BaseResponse>
{
    private readonly IPatientRepository _repository;
    private readonly IPatientContactRepository _repositorycontact;
    private readonly IPatientAddressRepository _repositoryaddress;
    private readonly IPatientTagRepository _repositorytag;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditPatientCommandHandler(IPatientContactRepository repositorycontact, IPatientAddressRepository repositoryaddress, IPatientTagRepository repositorytag, IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _repositorycontact = repositorycontact;
        _repositoryaddress = repositoryaddress;
        _repositorytag = repositorytag;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(EditPatientCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        Log log = new();

        try
        {
            var patient = _mapper.Map<Patient>(request.Dto);

            await _repository.Update(patient);
            await _repositorycontact.Delete(request.PatientId);
            await _repositoryaddress.Delete(request.PatientId);
            await _repositorytag.Delete(request.PatientId);

            response.Success = true;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (new { Id = patient.Id });
            if (request.Dto.Mobile == null)
            {

            }
            else
            {
                foreach (var mobile in request.Dto.Mobile)
                {
                    await _repository.InsertContactValueofPatientAsync(patient.Id, mobile);
                }
                foreach (var address in request.Dto.Address)
                {
                    await _repository.InsertAddressofPatientAsync(patient.Id, address);
                }
                foreach (var tag in request.Dto.Tag)
                {
                    await _repository.InsertTagofPatientAsync(patient.Id, tag);
                }
            }
            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors.Add(error.Message);

            log.Type = LogType.Error;
        }

        log.Header = response.StatusDescription;
        log.AdditionalData = response.Errors;

        await _logger.Log(log);

        return response;
    }
}
