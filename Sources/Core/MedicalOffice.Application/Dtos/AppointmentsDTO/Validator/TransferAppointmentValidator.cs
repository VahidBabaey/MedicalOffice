using FluentValidation;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Validator
{
    public class TransferAppointmentValidator : AbstractValidator<TransferAppointmentDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private IAppointmentRepository _appointmentRepository;
        private IRouteResolver _officeResolver;
        public TransferAppointmentValidator(
             IAppointmentRepository appointmentRepository,
             IRouteResolver officeResolver,
             IMedicalStaffRepository medicalStaffRepository,
             IServiceRepository serviceRepository
             )
        {
            _medicalStaffRepository = medicalStaffRepository;
            _appointmentRepository = appointmentRepository;
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            Include(new AppointmentIdValidator(_appointmentRepository, _officeResolver));
            Include(new MedicalStaffValidator(_medicalStaffRepository, _officeResolver));
            Include(new ServiceIdValidator(_serviceRepository, _officeResolver));

            RuleFor(x => x.StartTime)
            .NotEmpty()
                .WithMessage(ValidationMessage.Required.For("StartTime"));

            When(x => TimeOnly.TryParse(x.StartTime, out TimeOnly result), () =>
            {
                RuleFor(x => TimeOnly.Parse(x.StartTime))
                    .LessThan(x => TimeOnly.Parse(x.EndTime));
            }).Otherwise(() =>
            {
                RuleFor(x => x.StartTime)
                    .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                    .WithMessage(ValidationMessage.NotValid.For("StartTime"));
            });

            RuleFor(x => x.EndTime)
                .NotEmpty()
                    .WithMessage(ValidationMessage.Required.For("EndTime"));

            When(x => TimeOnly.TryParse(x.EndTime, out TimeOnly result), () =>
            {
                RuleFor(x => TimeOnly.Parse(x.EndTime))
                    .GreaterThan(x => TimeOnly.Parse(x.StartTime))
                        .WithMessage(ValidationMessage.GreaterThan.For("EndTime", "StartTime"));
            }).Otherwise(() =>
            {
                RuleFor(x => x.EndTime)
                    .Must(x => TimeOnly.TryParse(x, out TimeOnly result))
                    .WithMessage(ValidationMessage.NotValid.For("EndTime"));
            });
        }
    }
}
