using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class ServiceIdValidator : AbstractValidator<ServiceIdDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IContextResolver _officeResolver;

        public ServiceIdValidator(IContextResolver officeResolver, IServiceRepository serviceRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;

            Include(new IServiceIdValidator(_serviceRepository, _officeResolver));
        }
    }
}
