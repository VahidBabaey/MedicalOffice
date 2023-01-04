using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Features.AppointmentFeature.Helper;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, BaseResponse>
    {
        private readonly IValidator<AppointmentDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDeviceRepository _deviceRepository;

        private readonly string _requestTitle;

        public AddAppointmentCommandHandler(
            IValidator<AppointmentDTO> validator,
            ILogger logger,
            IMapper mapper,
            IAppointmentRepository appointmentRepository,
            IDeviceRepository deviceRepository
            )
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _deviceRepository = deviceRepository;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

            var validationResult = _validator.ValidateAsync(request.DTO, cancellationToken).Result;
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

            //var serviceId = _serviceRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.ServiceId);
            //if (serviceId == null)
            //{
            //    var error = "Service id does not exist!";
            //    await _logger.Log(new Log
            //    {
            //        Type = LogType.Error,
            //        Header = $"{_requestTitle} failed",
            //        AdditionalData = error
            //    });

            //    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            //}

            //var medicalStaff = _medicalStaffScheduleRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.MedicalStaffId);
            //if (medicalStaff == null)
            //{
            //    var error = "MedicalStaffId does not exist!";
            //    await _logger.Log(new Log
            //    {
            //        Type = LogType.Error,
            //        Header = $"{_requestTitle} failed",
            //        AdditionalData = error
            //    });

            //    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            //}

            try
            {
                var staffExistingAppointments = _appointmentRepository.GetByDateAndStaff(request.DTO.Date, medicalStaffId: request.DTO.MedicalStaffId).Result;
                var invalidAppointment = staffExistingAppointments.FirstOrDefault(x => !TimeHelper.isTimeValid(x, request.DTO.StartTime, request.DTO.EndTime));

                if (invalidAppointment != null)
                {
                    var error = "Staff isn't free in requested Time";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }

                if (request.DTO.RoomId != null && request.DTO.DeviceId != null)
                {
                    var roomHasDevice = _deviceRepository.GetDevicesByRoomId((Guid)request.DTO.RoomId).Result
                        .Contains(new Device { Id = (Guid)request.DTO.DeviceId });

                    if (!roomHasDevice)
                    {
                        var error = "Device isn't exist in this room";
                        await _logger.Log(new Log
                        {
                            Type = LogType.Error,
                            Header = $"{_requestTitle} failed",
                            AdditionalData = error
                        });
                        return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                    }

                    var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(
                        request.DTO.Date,
                        deviceId: request.DTO.DeviceId,
                        roomId: request.DTO.RoomId).Result;

                    invalidAppointment = deviceExistingAppointments.FirstOrDefault(x => !TimeHelper.isTimeValid(x, request.DTO.StartTime, request.DTO.EndTime));

                    if (invalidAppointment != null)
                    {
                        var error = "Device isn't free in requested Time";
                        await _logger.Log(new Log
                        {
                            Type = LogType.Error,
                            Header = $"{_requestTitle} failed",
                            AdditionalData = error
                        });
                        return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                    }
                }

                if (request.DTO.DeviceId == null && request.DTO.RoomId != null)
                {
                    var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(request.DTO.Date, roomId: request.DTO.RoomId).Result;
                    invalidAppointment = deviceExistingAppointments.FirstOrDefault(x => !TimeHelper.isTimeValid(x, request.DTO.StartTime, request.DTO.EndTime));

                    if (invalidAppointment != null)
                    {
                        var error = "Room isn't free in requested Time";
                        await _logger.Log(new Log
                        {
                            Type = LogType.Error,
                            Header = $"{_requestTitle} failed",
                            AdditionalData = error
                        });
                        return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                    }
                }

                var appointment = _mapper.Map<Appointment>(request.DTO);
                appointment.OfficeId = request.OfficeId;
                var result = _appointmentRepository.Add(appointment).Result;

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { result.Id }
                });

                return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { result.Id });
            }
            catch (Exception error)
            {
                throw error;
            }

        }
    }
}
