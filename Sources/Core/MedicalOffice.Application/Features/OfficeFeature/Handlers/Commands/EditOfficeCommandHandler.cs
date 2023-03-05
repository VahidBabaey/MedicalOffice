using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.OfficeFeature.Handlers.Commands
{
    public class EditOfficeCommandHandler : IRequestHandler<EditOfficeCommand, BaseResponse>
    {
        private readonly IValidator<UpdateOfficeDTO> _validator;
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;
        public EditOfficeCommandHandler(
            IValidator<UpdateOfficeDTO> validator,
            IUserResolverService userResolverService,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IOfficeRepository officeRepository,
            ILogger logger,
            IMapper mapper
            )
        {
            _validator = validator;
            _officeRepository = officeRepository;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(EditOfficeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var office = _mapper.Map<Office>(request.DTO);

            await _officeRepository.Update(office);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = office.Id
            }) ;

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", office.Id);
        }
    }
}
