using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO.Validator;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Handlers.Commands
{

    public class AddPatientReferralFormCommandHandler : IRequestHandler<AddPatientReferralFormCommand, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IValidator<PatientReferralFormDTO> _validator;
        private readonly IPatientReferralFormRepository _patientreferralformrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddPatientReferralFormCommandHandler(IOfficeRepository officeRepository, IValidator<PatientReferralFormDTO> validator, IPatientReferralFormRepository patientreferralformrepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _validator = validator;
            _patientreferralformrepository = patientreferralformrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddPatientReferralFormCommand request, CancellationToken cancellationToken)
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

            var validationCommitmentName = await _patientreferralformrepository.CheckExistPatientReferralFormForm(request.DTO.ReferralReason, request.DTO.DateSolar);

            if (validationCommitmentName)
            {
                var error = "Name Must be Unique";
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
                    var patientreferralform = _mapper.Map<PatientReferralForm>(request.DTO);

                    patientreferralform = await _patientreferralformrepository.Add(patientreferralform);

                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} succeded",
                        AdditionalData = patientreferralform.Id
                    });
                    return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", patientreferralform.Id);
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
