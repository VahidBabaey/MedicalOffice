using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO.Validator
{
    public class UpdateServiceRoomValidator : AbstractValidator<UpdateServiceRoomDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IQueryStringResolver _officeResolver;
        private readonly IRoomRepository _serviceRoomRepository;

        public UpdateServiceRoomValidator(IQueryStringResolver officeResolver, IServiceRepository serviceRepository, IRoomRepository serviceRoomRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;
            _serviceRoomRepository = serviceRoomRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            Include(new ServiceIdsValidator(_officeResolver, _serviceRepository));
            Include(new RoomNameValidator(_serviceRoomRepository, _officeResolver));

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (Id, Token) =>
                {
                    return await _serviceRoomRepository.isServiceRoomExist(officeId, Id);
                });
        }
    }
}