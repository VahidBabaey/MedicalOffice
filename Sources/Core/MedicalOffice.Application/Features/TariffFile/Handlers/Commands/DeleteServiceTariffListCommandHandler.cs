using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class DeleteServiceTariffListCommandHandler : IRequestHandler<DeleteTariffListCommand, BaseResponse>
{
    private readonly IValidator<TariffListIdDTO> _validator;
    private readonly IOfficeRepository _officeRepository;
    private readonly IServiceTariffRepository _tariffrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteServiceTariffListCommandHandler(IValidator<TariffListIdDTO> validator,IOfficeRepository officeRepository, IServiceTariffRepository tariffrepository, ILogger logger)
    {
        _validator = validator;
        _officeRepository = officeRepository;
        _tariffrepository = tariffrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteTariffListCommand request, CancellationToken cancellationToken)
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

        foreach (var item in request.DTO.TariffId)
        {
            await _tariffrepository.SoftDelete(item);
        }

        await _logger.Log(new Log
        {
            Type = LogType.Success,
            Header = $"{_requestTitle} succeded",
        });
        return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded");

    }
}
