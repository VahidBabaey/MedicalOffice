using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Features.ServiceDurationScheduling.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceDurationScheduling.Handlers.Commands
{
    public class AddServiceDurationCommandsHandler : IRequestHandler<AddServiceDurationCommand, BaseResponse>
    {
        private readonly IValidator<ServiceDurationDTO> _validator;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private readonly string _requestTitle;

        public AddServiceDurationCommandsHandler(
            IValidator<ServiceDurationDTO> validator,
            IMapper mapper,
            ILogger logger,
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository,
            IServiceDurationRepositopry serviceDurationRepository)
        {
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;
            _serviceDurationRepository = serviceDurationRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddServiceDurationCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var existingMedicalStaff = await _medicalStaffRepository.CheckMedicalStaffExist(request.DTO.MedicalStaffId, request.OfficeId);
            if (!existingMedicalStaff)
            {
                var error = new ArgumentException("This MedicalStaff does not exist!");
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error.Message
                });

                return responseBuilder.Faild(HttpStatusCode.NotFound,
                    $"{_requestTitle} failed",
                    error.Message);
            }

            var existingService = _serviceRepository.GetAll().Result.Any(x =>
            x.Id == request.DTO.ServiceId &&
            x.OfficeId == request.OfficeId);

            if (!existingService)
            {
                var error = new ArgumentException("This Service does not exist!");
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error.Message
                });

                return responseBuilder.Faild(HttpStatusCode.NotFound,
                    $"{_requestTitle} failed",
                    error.Message);
            }

            var serviceDuration = _mapper.Map<ServiceDuration>(request.DTO);

            try
            {
                var newServiceDuration = _serviceDurationRepository.Add(serviceDuration).Result;

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = newServiceDuration
                }); ;

                return responseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    newServiceDuration.Id);
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error.Message
                });

                return responseBuilder.Faild(HttpStatusCode.InternalServerError,
                    $"{_requestTitle} failed",
                    error.Message);
            }
        }
    }
}
