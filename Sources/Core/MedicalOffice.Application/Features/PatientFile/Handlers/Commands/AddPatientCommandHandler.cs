using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO.Validators;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, BaseCommandResponse>
{
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddPatientCommandHandler(IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseCommandResponse> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {

        BaseCommandResponse response = new();

        AddPatientValidator validator = new();

        Log log = new();

        var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.Message = $"{_requestTitle} failed";
            response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            log.Type = LogType.Error;
        }
        else
        {
            try
            {
                var patient = _mapper.Map<Patient>(request.Dto);

                patient = await _repository.Add(patient);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = patient.Id });
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
                response.Message = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }
        }

        log.Header = response.Message;
        log.Messages = response.Errors;

        await _logger.Log(log);

        return response;
    }


}









