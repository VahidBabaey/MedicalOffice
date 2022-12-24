using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Features.CashFile.Handlers.Commands;

public class AddCashDebtCommandHandler : IRequestHandler<AddCashDebtCommand, BaseResponse>
{
    private readonly IReceptionRepository _repositoryReception;
    private readonly ICashRepository _repository;
    private readonly IReceptionDebtRepository _repositoryReceptionDebt;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddCashDebtCommandHandler(IReceptionDebtRepository repositoryReceptionDebt, IReceptionRepository repositoryReception, ICashRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _repositoryReception = repositoryReception;
        _repositoryReceptionDebt = repositoryReceptionDebt;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddCashDebtCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        AddCashValidator validator = new();

        Log log = new();

        var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            log.Type = LogType.Error;
        }
        else
        {
            try
            {
                var cash = _mapper.Map<Cash>(request.DTO);

                cash = await _repository.Add(cash);

                var receptionDebt = await _repositoryReception.CreateNewReceptionDebt(request.DTO.Recieved, request.DTO.OfficeId, request.DTO.ReceptionId);

                var receptionDetailDebt = await _repositoryReception.CreateNewReceptionDetailDebt(request.DTO.Recieved, request.DTO.OfficeId, request.DTO.ReceptionId);

                await _repositoryReceptionDebt.AddReceptionDebt(request.DTO.ReceptionId, receptionDetailDebt.Id, request.DTO.OfficeId, request.DTO.Recieved);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = cash.Id });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }
        }

        log.Header = response.StatusDescription;
        log.AdditionalData = response.Errors;

        await _logger.Log(log);

        return response;
    }
}

