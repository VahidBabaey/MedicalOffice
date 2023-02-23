using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;
using static System.Collections.Specialized.BitVector32;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Commands
{

    public class EditServiceCommandHandler : IRequestHandler<EditServiceCommand, BaseResponse>
    {
        private readonly ISectionRepository _sectionrepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IValidator<UpdateServiceDTO> _validator;
        private readonly IServiceRepository _servicerepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditServiceCommandHandler(ISectionRepository sectionrepository, IOfficeRepository officeRepository, IValidator<UpdateServiceDTO> validator, IServiceRepository servicerepository, IMapper mapper, ILogger logger)
        {
            _sectionrepository = sectionrepository;
            _officeRepository = officeRepository;
            _validator = validator;
            _servicerepository = servicerepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditServiceCommand request, CancellationToken cancellationToken)
        {

            var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

            if (!validationOfficeId)
            {
                var error = "OfficeID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var validationServiceId = await _servicerepository.CheckExistServiceId(request.OfficeId ,request.DTO.Id);

            if (!validationServiceId)
            {
                var error = "ID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var validationSectionId = await _sectionrepository.CheckExistSectionId(request.OfficeId, request.DTO.SectionId);

            if (!validationSectionId)
            {
                var error = "SectionID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

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
            else
            {
                try
                {
                    var service = _mapper.Map<Service>(request.DTO);
                    service.OfficeId = request.OfficeId;

                    await _servicerepository.Update(service);

                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} succeded",
                        AdditionalData = service.Id
                    });
                    return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", service.Id);
                }
                catch (Exception error)
                {
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error.Message
                    });
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
                }
            }
        }
    }

}
