using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Dtos.ShiftDTO.Validators;
using MedicalOffice.Application.Features.ShiftFile.Requests.Command;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ShiftFile.Handlers.Command
{

    public class AddShiftCommandHandler : IRequestHandler<AddShiftCommand, BaseResponse>
    {
        private readonly IValidator<ShiftDTO> _validator;
        private readonly IShiftRepository _repository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddShiftCommandHandler(IValidator<ShiftDTO> validator, IOfficeRepository officeRepository,  IShiftRepository repository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddShiftCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

            if (!validationOfficeId)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add("OfficeID isn't exist");

                log.Type = LogType.Error;
                return response;
            }

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
                    var shift = _mapper.Map<Shift>(request.DTO);
                    shift.OfficeId = request.OfficeId;

                    shift = await _repository.Add(shift);

                    response.Success = true;
                    response.StatusDescription = $"{_requestTitle} succeded";
                    response.Data = (new { Id = shift.Id });

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
