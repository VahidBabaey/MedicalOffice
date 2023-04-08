using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
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
        private readonly IContextResolver _officeResolver;
        private readonly IRoomRepository _serviceRoomRepository;

        public UpdateServiceRoomValidator(IContextResolver officeResolver, IServiceRepository serviceRepository, IRoomRepository serviceRoomRepository)
        {
            _officeResolver = officeResolver;
            _serviceRepository = serviceRepository;
            _serviceRoomRepository = serviceRoomRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            Include(new IServiceIdsValidator(_officeResolver, _serviceRepository));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("ورود نام اتاق ضروری است");
            RuleFor(x => x)
                .MustAsync(async (roomName, token) =>
                {
                    return await _serviceRoomRepository.isNameUniqeDuringUpdate(roomName, officeId);
                })
                //.WithMessage("{PropertyName} is not uniqe");
                .WithMessage("نام اتاق باید یکتا باشد");

            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("ورود شناسه مطب ضروری است")
                .MustAsync(async (Id, Token) =>
                {
                    return await _serviceRoomRepository.isServiceRoomExist(officeId, Id);
                });
        }
    }
}