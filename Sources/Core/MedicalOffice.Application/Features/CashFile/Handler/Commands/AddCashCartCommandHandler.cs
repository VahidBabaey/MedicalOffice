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

public class AddCashCartCommandHandler : IRequestHandler<AddCashCartCommand, BaseResponse>
{
    private readonly IValidator<CashCartDTO> _validator;
    private readonly ICashCartRepository _cashcartrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddCashCartCommandHandler(IValidator<CashCartDTO> validator, ICashCartRepository cashcartrepository, ILogger logger)
    {
        _validator = validator;
        _cashcartrepository = cashcartrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddCashCartCommand request, CancellationToken cancellationToken)
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
                var cashcart = _cashcartrepository.AddCashCartForAnyReceptionDetail(request.OfficeId, request.DTO.ReceptionId, request.DTO.CartNumber, request.DTO.Cost, request.DTO.BankId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = cashcart
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", cashcart);
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

