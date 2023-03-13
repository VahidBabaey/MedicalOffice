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

public class AddReceptionDetailCommandHandler : IRequestHandler<AddReceptionDetailCommand, BaseResponse>
{
    private readonly IValidator<ReceptionDetailDTO> _validator;
    private readonly IReceptionRepository _receptionrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddReceptionDetailCommandHandler(IValidator<ReceptionDetailDTO> validator, IReceptionRepository receptionrepository, ILogger logger)
    {
        _validator = validator;
        _receptionrepository = receptionrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddReceptionDetailCommand request, CancellationToken cancellationToken)
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
                var receptionDetail = await _receptionrepository.AddReceptionService(request.OfficeId, request.DTO.ReceptionType, request.DTO.PatientId, request.DTO.ReceptionId, request.DTO.ServiceId, request.DTO.ServiceCount, request.DTO.InsuranceId, request.DTO.AdditionalInsuranceId, request.DTO.MembershipId, request.DTO.MedicalStaffs, request.DTO.Costd);
                await _receptionrepository.UpdatereceptionDescription((Guid)request.DTO.ReceptionId, request.Description);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = receptionDetail.Id
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", receptionDetail.Id);
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
