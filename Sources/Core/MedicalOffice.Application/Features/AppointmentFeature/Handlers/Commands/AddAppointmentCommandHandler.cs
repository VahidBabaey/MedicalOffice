using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, BaseResponse>
    {
        private readonly IValidator<AppointmentDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;

        private readonly string _requestTitle;

        public AddAppointmentCommandHandler(
            IValidator<AppointmentDTO> validator,
            ILogger logger,
            IMapper mapper,
            IAppointmentRepository appointmentRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository
            )
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
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

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            #region checkValidTimeApi
            //get staff date time
            //bool validTime = _staffWorkHourRepository.checkTimeIsInStaffWorkHours();
            //if(false)
            //add warning to data

            //get service duration time
            //var serviceDuration = await _serviceDurationRepository.getByIdAndMedicalStaffId();
            //if(serviceDuration>EndTime-StartTime)
            //add warning to data
            #endregion

            var staffExistingAppointments = _appointmentRepository.GetByDateAndStaff(request.DTO.date, medicalStaffId: request.DTO.MedicalStaffId).Result;
            var validAppointment = staffExistingAppointments.FirstOrDefault(x => !isValidTime(x, request));

            if (validAppointment != null)
            {
                var error = "Staff isn't free in requested time";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            if (request.DTO.DeviceId != null)
            {
                var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(request.DTO.date, deviceId: request.DTO.DeviceId, roomId: request.DTO.RoomId).Result;
                validAppointment = deviceExistingAppointments.FirstOrDefault(x => !isValidTime(x, request));

                if (validAppointment != null)
                {
                    var error = "Room or device isn't free in requested time";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }

            var medicalStaff = _medicalStaffRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.MedicalStaffId);
            if (medicalStaff == null)
            {
                var error = "MedicalStaffId does not exist!";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var serviceId = _serviceRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.ServiceId);
            if (serviceId == null)
            {
                var error = "Service id does not exist!";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var appointment = _mapper.Map<Appointment>(request.DTO);
            var result = _appointmentRepository.Add(appointment);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { result.Id }
            });

            return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { result.Id });
        }

        static bool isValidTime(AppointmentDetailsDTO time, AddAppointmentCommand request)
        {
            var serviceTime = TimeOnly.Parse(request.DTO.EndTime) - TimeOnly.Parse(request.DTO.StartTime);

            if (TimeOnly.Parse(time.StartTime) < TimeOnly.Parse(request.DTO.EndTime) &&
                TimeOnly.Parse(request.DTO.StartTime) < TimeOnly.Parse(time.EndTime))
                return false;

            if (TimeOnly.Parse(time.StartTime) == TimeOnly.Parse(request.DTO.StartTime) ||
                TimeOnly.Parse(time.EndTime) == TimeOnly.Parse(request.DTO.EndTime))
                return false;

            if (
                TimeOnly.Parse(request.DTO.StartTime) - TimeOnly.Parse(time.StartTime) < serviceTime ||
                TimeOnly.Parse(request.DTO.EndTime) - TimeOnly.Parse(time.EndTime) < serviceTime)
                return false;

            return true;
        }
    }
}
