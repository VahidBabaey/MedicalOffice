using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class SearchByPatientQueryHandler : IRequestHandler<SearchByPatientQuery, BaseResponse>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMedicalStaffScheduleRepository _staffScheduleRepository;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;

        private readonly string _requestTitle;
        public SearchByPatientQueryHandler(ILogger logger,
        IMapper mapper,
        IAppointmentRepository appointmentRepository,
        IMedicalStaffRepository medicalStaffRepository,
        IServiceRepository serviceRepository,
        IMedicalStaffScheduleRepository staffScheduleRepository,
        IServiceDurationRepositopry serviceDurationRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;
            _staffScheduleRepository = staffScheduleRepository;
            _serviceDurationRepository = serviceDurationRepository;
        }
        public async Task<BaseResponse> Handle(SearchByPatientQuery request, CancellationToken cancellationToken)
        {

            var responseBuilder = new ResponseBuilder();
            var patientAppointments = await _appointmentRepository.searchPatientAappointments(request.Input, request.Date, request.OfficeId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = patientAppointments
            });

            return responseBuilder.Success(HttpStatusCode.OK,
                $"{_requestTitle} succeeded",
                patientAppointments);

            throw new NotImplementedException();
        }
    }
}
