using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO.Validators;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Commands
{
    public class AddServiceCommandHandler : IRequestHandler<AddServiceCommand, BaseResponse>
    {
        private readonly IValidator<ServiceDTO> _validator;
        private readonly IServiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddServiceCommandHandler(IValidator<ServiceDTO> validator, IServiceRepository repository, IMapper mapper, ILogger logger)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddServiceCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                try
                {
                    var service = _mapper.Map<Service>(request.DTO);
                    service.OfficeId = request.OfficeId;

                    service = await _repository.Add(service);

                    response.Success = true;
                    response.StatusDescription = $"{_requestTitle} succeded";
                    response.Data = (new { Id = service.Id });

                    log.Type = LogType.Success;
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.StatusDescription = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
