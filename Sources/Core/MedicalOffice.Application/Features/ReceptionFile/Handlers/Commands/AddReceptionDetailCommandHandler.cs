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
    private readonly IValidator<ReceptionDetailDTO> _validator;
    private readonly IReceptionRepository _repository;
    private readonly ICashRepository _repositoryCash;
    private readonly IReceptionDebtRepository _repositoryDebt;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddReceptionDetailCommandHandler(IValidator<ReceptionDetailDTO> validator, IReceptionDebtRepository repositoryDebt, ICashRepository repositoryCash, IReceptionRepository repository, IMapper mapper, ILogger logger)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _repositoryCash = repositoryCash;
        _repositoryDebt = repositoryDebt;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddReceptionDetailCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        Log log = new();

        var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

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
                var receptionDetail = await _repository.AddReceptionService(request.DTO.ReceptionId, request.DTO.ServiceId, request.DTO.ServiceCount, request.DTO.InsuranceId, request.DTO.AdditionalInsuranceId, request.DTO.Cost, request.DTO.ReceptionDiscountId, request.DTO.MedicalStaffs);

                await _repositoryCash.AddCashForAnyReceptionDetail(receptionDetail.OfficeId, receptionDetail.ReceptionId, receptionDetail.Cost);

                if (receptionDetail.Debt > 0)
                {

                    //await _repositoryDebt.AddReceptionDebt(receptionDetail.ReceptionId, receptionDetail.Id, receptionDetail.OfficeId, receptionDetail.Debt);

                }

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = receptionDetail });

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
