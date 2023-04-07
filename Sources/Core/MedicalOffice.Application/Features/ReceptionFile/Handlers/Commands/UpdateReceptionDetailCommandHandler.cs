using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO.Validators;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Commands;

public class UpdateReceptionDetailCommandHandler : IRequestHandler<UpdateReceptionDetailCommand, BaseResponse>
{
    private readonly IValidator<UpdateReceptionDetailDTO> _validator;
    private readonly IReceptionRepository _receptionrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public UpdateReceptionDetailCommandHandler(IValidator<UpdateReceptionDetailDTO> validator, IReceptionRepository receptionrepository, ILogger logger)
    {
        _validator = validator;
        _receptionrepository = receptionrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(UpdateReceptionDetailCommand request, CancellationToken cancellationToken)
    {

        var validationReceptionDetailId = await _receptionrepository.CheckExistReceptionDetailId(request.OfficeId, request.ReceptiodDetailId);

        if (!validationReceptionDetailId)
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
                var reception = await _receptionrepository.UpdateReceptionService(request.ReceptiodDetailId, request.OfficeId, request.DTO.ReceptionId, request.DTO.ServiceId, request.DTO.ServiceCount, request.DTO.InsuranceId, request.DTO.AdditionalInsuranceId, request.DTO.MedicalStaffs, request.DTO.Recieved, request.DTO.OrganShare, request.DTO.PatientShare, request.DTO.AdditionalInsuranceShare, request.DTO.Tariff, request.DTO.Discount);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = reception
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", reception);
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
