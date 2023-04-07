using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO.Validator
{
    public class AddServiceRoomValidator : AbstractValidator<AddServiceRoomDTO>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IContextResolver _officeResolver;
        private readonly IRoomRepository _serviceRoomRepository;

        public AddServiceRoomValidator(
            IContextResolver officeResolver,
            IServiceRepository serviceRepository,
            IRoomRepository serviceRoomRepository
            )
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;
            _serviceRoomRepository = serviceRoomRepository;

            Include(new IServiceIdsValidator(_officeResolver, _serviceRepository));
            Include(new RoomNameValidator(_serviceRoomRepository, _officeResolver));
        }
    }
}