using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Features.TariffFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Commands
{
    public class AddServiceTariffCommandHandler : IRequestHandler<AddServiceTariffCommand, BaseResponse>
    {
        private readonly IValidator<TariffDTO> _validator;
        private readonly IOfficeRepository _officeRepository;
        private readonly IServiceTariffRepository _tariffrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;
        private readonly IServiceRepository _serviceRepository;

        public AddServiceTariffCommandHandler(
            IValidator<TariffDTO> validator,
            IOfficeRepository officeRepository,
            IServiceTariffRepository tariffrepository,
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger logger)
        {
            _serviceRepository = serviceRepository;
            _officeRepository = officeRepository;
            _validator = validator;
            _tariffrepository = tariffrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddServiceTariffCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            if (request.DTO.Tariffs.Count == 0)
            {
                var existingService = await _serviceRepository.GetById(request.DTO.ServiceId);
                existingService.TariffInReceptionTime = true;
                await _serviceRepository.Update(existingService);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { });
            }

            var tariffs = _mapper.Map<List<Tariff>>(request.DTO.Tariffs);

            foreach (var item in tariffs)
            {
                item.OfficeId = request.OfficeId;
                item.ServiceId = request.DTO.ServiceId;
            }

            await _tariffrepository.AddRange(tariffs);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = tariffs.Select(x => x.Id)
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", tariffs.Select(x => x.Id));
        }
    }
}