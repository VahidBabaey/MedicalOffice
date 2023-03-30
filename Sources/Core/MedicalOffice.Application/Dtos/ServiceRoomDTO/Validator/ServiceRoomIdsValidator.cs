using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO.Validator
{
    public class ServiceRoomIdsValidator : AbstractValidator<ServiceRoomIdsDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IContextResolver _officeResolver;
        private readonly IRoomRepository _serviceRoomRepository;

        public ServiceRoomIdsValidator(
            IContextResolver officeResolver,
            IServiceRepository serviceRepository,
            IRoomRepository serviceRoomRepository
            )
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;
            _serviceRoomRepository = serviceRoomRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.RoomIds)
                .NotEmpty()
                .WithMessage("ورود شناسه اتاق ضروری است")
                .MustAsync(async (serviceRoomIds, Token) =>
                {
                    foreach (var item in serviceRoomIds)
                    {
                        var isExist = await _serviceRoomRepository.isServiceRoomExist(officeId, item);

                        if (!isExist)
                        {
                            return false;
                        }
                    }
                    return true;
                });
        }
    }
}