﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ReceptionDTO.Validators;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Commands;
public class AddReceptionDiscountCommandHandler : IRequestHandler<AddReceptionDiscountCommand, BaseResponse>
{
    private readonly IReceptionDiscountRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddReceptionDiscountCommandHandler(IReceptionDiscountRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddReceptionDiscountCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        AddReceptionDiscountValidator validator = new();

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
                var receptionDiscount = _mapper.Map<ReceptionDiscount>(request.DTO);

                receptionDiscount = await _repository.Add(receptionDiscount);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data=(new { Id = receptionDiscount.Id });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
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
