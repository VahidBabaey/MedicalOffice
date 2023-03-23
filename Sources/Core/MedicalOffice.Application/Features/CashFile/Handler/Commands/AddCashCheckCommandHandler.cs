using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.CashFile.Handlers.Commands;

public class AddCashCheckCommandHandler : IRequestHandler<AddCashCheckCommand, BaseResponse>
{
    private readonly IValidator<CashCheckDTO> _validator;
    private readonly ICashCheckRepository _cashcheckrepository;
    private readonly ICashRepository _cashrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddCashCheckCommandHandler(ICashRepository cashrepository, IValidator<CashCheckDTO> validator, ICashCheckRepository cashcheckrepository, IMapper mapper, ILogger logger)
    {
        _validator = validator;
        _cashrepository = cashrepository;
        _cashcheckrepository = cashcheckrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddCashCheckCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }
        else
        {
            try
            {

                var cashcheck = _cashcheckrepository.AddCashCheckForAnyReceptionDetail(request.OfficeId, request.DTO.ReceptionId, request.DTO.Cost, request.DTO.BankId, request.DTO.Branch);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = cashcheck
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", cashcheck);
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
}

