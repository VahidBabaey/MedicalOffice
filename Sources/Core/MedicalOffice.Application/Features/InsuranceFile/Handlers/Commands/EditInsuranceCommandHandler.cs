using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO.Validators;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.InsuranceFile.Handlers.Commands
{

    public class EditInsuranceCommandHandler : IRequestHandler<EditInsuranceCommand, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IValidator<UpdateInsuranceDTO> _validator;
        private readonly IInsuranceRepository _insurancerepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditInsuranceCommandHandler(IOfficeRepository officeRepository, IValidator<UpdateInsuranceDTO> validator, IInsuranceRepository insurancerepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _validator = validator;
            _insurancerepository = insurancerepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditInsuranceCommand request, CancellationToken cancellationToken)
        {

            var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

            if (!validationOfficeId)
            {
                var error = "OfficeID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var validationInsuranceId = await _insurancerepository.CheckExistInsuranceId(request.OfficeId, request.DTO.Id);

            if (!validationInsuranceId)
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
                    var insurance = _mapper.Map<Insurance>(request.DTO);
                    insurance.OfficeId = request.OfficeId;

                    await _insurancerepository.Update(insurance);

                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} succeded",
                        AdditionalData = insurance.Id
                    });
                    return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", insurance.Id);
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
}
