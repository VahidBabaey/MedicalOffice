using FluentValidation;
using MedicalOffice.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.AppointmentsDTO.Commons
{
    public class IServiceIdValidator : AbstractValidator<IServiceIdDTO>
    {
        private readonly IServiceRepository _serviceRepository;

        public IServiceIdValidator(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;

            RuleFor(x => x.ServiceId)
                .NotEmpty()
                .MustAsync(async (id, token) =>
                      {
                          var leaveTypeExists = await _serviceRepository.GetById(id);
                          if (leaveTypeExists != null)
                          {
                              return true;
                          }
                          return false;
                      })
                .WithMessage("{PropertyName} isn't exist");
        }

        //private bool isServiceExist(Guid serviceId, IServiceRepository _serviceRepository)
        //{
        //    var existingService = _serviceRepository.GetById(serviceId);

        //    if (existingService != null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
