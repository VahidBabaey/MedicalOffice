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

public class DeleteReceptionDetailCommandHandler : IRequestHandler<DeleteReceptionDetailCommand, BaseResponse>
{
    private readonly IReceptionRepository _receptionrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteReceptionDetailCommandHandler(IReceptionRepository receptionrepository, ILogger logger)
    {
        _receptionrepository = receptionrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteReceptionDetailCommand request, CancellationToken cancellationToken)
    {

        try
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

            await _receptionrepository.DeleteReceptionService(request.ReceptiodDetailId, request.OfficeId);

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
