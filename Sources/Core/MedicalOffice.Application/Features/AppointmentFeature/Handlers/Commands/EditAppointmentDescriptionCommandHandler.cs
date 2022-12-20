using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models;
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
    public class EditAppointmentDescriptionCommandHandler : IRequestHandler<EditAppointmentDescriptionCommand, BaseResponse>
    {
        private readonly IValidator<AppointmentDescriptionDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        private readonly string _requestTitle;

        public EditAppointmentDescriptionCommandHandler(
            IValidator<AppointmentDescriptionDTO> validator, 
            ILogger logger, 
            IMapper mapper, 
            IAppointmentRepository appointmentRepository
            )
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditAppointmentDescriptionCommand request, CancellationToken cancellationToken)
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

            var existingAppointment = _appointmentRepository.GetById(request.DTO.AppointmentId).Result;

            if (existingAppointment==null)
            {
                throw new ArgumentException("");
            }

            var newAppointment = _mapper.Map<Appointment>(request.DTO);

            await _appointmentRepository.Update(newAppointment);

            await _appointmentRepository.Update(newAppointment);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = newAppointment.Id
            });

            return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", newAppointment.Id);
        }
    }
}
