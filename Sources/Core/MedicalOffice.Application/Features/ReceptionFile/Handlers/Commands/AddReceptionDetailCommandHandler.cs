using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Reception;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Commands;

public class AddReceptionDetailCommandHandler : IRequestHandler<AddReceptionDetailCommand, BaseResponse>
{
    private readonly IReceptionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddReceptionDetailCommandHandler(IReceptionRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddReceptionDetailCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        AddReceptionDetailValidator validator = new();

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
                //var reception = _mapper.Map<Reception>(request.DTO);

                await _repository.AddReceptionService(request.DTO.ReceptionId, request.DTO.ServiceId, request.DTO.ServiceCount, request.DTO.InsuranceId, request.DTO.AdditionalInsuranceId, request.DTO.Cost, request.DTO.ReceptionDiscountId, request.DTO.MedicalStaffs);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                //response.Data = (new { Id = reception.Id });

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
