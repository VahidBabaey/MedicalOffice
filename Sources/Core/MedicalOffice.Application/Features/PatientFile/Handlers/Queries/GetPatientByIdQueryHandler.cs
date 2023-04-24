using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatietByIdQuery, BaseResponse>
    {
        private readonly IValidator<PatientIdDTO> _validator;
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger _logger;

        public GetPatientByIdQueryHandler(
             IValidator<PatientIdDTO> validator,
             IPatientRepository patientRepository,
             ILogger logger
            )
        {
            _validator = validator;
            _patientRepository = patientRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(GetPatietByIdQuery request, CancellationToken cancellationToken)
        {
            var failedMessage = ResponseBuilder.FailedMessage(GetType().Name);
            var successMessage = ResponseBuilder.SuccessMessage(GetType().Name);

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Header = failedMessage,
                    Type = LogType.Error,
                    AdditionalData = validationResult.Errors.Select(x => x.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(
                    HttpStatusCode.BadRequest,
                    failedMessage,
                    validationResult.Errors.Select(x => x.ErrorMessage).ToArray());
            }

            try
            {
                var patient = await _patientRepository.GetPatientById(request.DTO.PatientId, request.OfficeId);

                await _logger.Log(new Log
                {
                    Header = successMessage,
                    Type = LogType.Success,
                    AdditionalData = patient
                });

                return ResponseBuilder.Success(
                    HttpStatusCode.OK,
                    successMessage,
                    patient);
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Header = failedMessage,
                    Type = LogType.Error,
                    AdditionalData = error.Message
                });

                return ResponseBuilder.Faild(
                    HttpStatusCode.InternalServerError,
                    failedMessage,
                    error.Message);
            }
        }
    }
}
