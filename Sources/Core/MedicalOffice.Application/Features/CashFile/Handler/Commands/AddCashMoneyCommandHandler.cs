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

public class AddCashMoneyCommandHandler : IRequestHandler<AddCashMoneyCommand, BaseResponse>
{
    private readonly IValidator<CashMoneyDTO> _validator;
    private readonly ICashMoneyRepository _cashmoneyrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;
    private readonly IReceptionRepository _receptionRepository;

    public AddCashMoneyCommandHandler(IReceptionRepository receptionRepository,ICashMoneyRepository cashmoneyrepository, IValidator<CashMoneyDTO> validator, ILogger logger)
    {
        _receptionRepository = receptionRepository;
        _validator = validator;
        _cashmoneyrepository = cashmoneyrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddCashMoneyCommand request, CancellationToken cancellationToken)
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
        //if (request.DTO.Cost > request.DTO.TotalDebt)
        //{
        //    var error = "مبلغ پرداختی بیشتر از بدهی میباشد";
        //    await _logger.Log(new Log
        //    {
        //        Type = LogType.Error,
        //        Header = $"{_requestTitle} failed",
        //        AdditionalData = error
        //    });
        //    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        //}
        //else
        //{
        try
        {
            var reception = await _receptionRepository.GetById(request.DTO.ReceptionId);
            if (request.DTO.Cost > reception.TotalDebt)
            {
                var error = "مبلغ پرداختی بیشتر از بدهی میباشد";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var cashmoney = await _cashmoneyrepository.AddCashMoneyForAnyReceptionDetail(request.OfficeId, request.DTO.ReceptionId, request.DTO.Cost);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = cashmoney
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", cashmoney);
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
//}