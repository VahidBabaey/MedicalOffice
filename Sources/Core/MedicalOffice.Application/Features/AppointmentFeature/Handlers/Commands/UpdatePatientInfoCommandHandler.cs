using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class EditAppointmentPatientCommandHandler : IRequestHandler<EditAppointmentPatientCommand, BaseResponse>
    {
        private readonly IValidator<UpdateAppointmentPatientInfoDto> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly string _requestTitle;

        public EditAppointmentPatientCommandHandler(IValidator<UpdateAppointmentPatientInfoDto> validator, ILogger logger, IMapper mapper, IAppointmentRepository appointmentRepository, IMedicalStaffRepository medicalStaffRepository, IServiceRepository serviceRepository, IDeviceRepository deviceRepository)
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;
            _deviceRepository = deviceRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(EditAppointmentPatientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var existingAppointment = _appointmentRepository.GetById(request.DTO.AppointmentId).Result;

            existingAppointment.PatientName = request.DTO.PatientName;
            existingAppointment.PatientLastName = request.DTO.PatientLastName;
            existingAppointment.PhoneNumber = request.DTO.PhoneNumber;
            existingAppointment.NationalId = request.DTO.NationalId;
            existingAppointment.ReferrerId = request.DTO.ReferrerId;
            existingAppointment.Description = request.DTO.Description;

            await _appointmentRepository.Update(existingAppointment);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = existingAppointment.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", existingAppointment.Id);
        }
    }
}