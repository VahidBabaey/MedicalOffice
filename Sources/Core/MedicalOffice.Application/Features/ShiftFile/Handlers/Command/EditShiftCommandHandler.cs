using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Features.ShiftFile.Requests.Command;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ShiftFile.Handlers.Command
{

    public class EditShiftCommandHandler : IRequestHandler<EditShiftCommand, BaseResponse>
    {
        private readonly IValidator<UpdateShiftDTO> _validator;
        private readonly IShiftRepository _repository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditShiftCommandHandler(IOfficeRepository officeRepository, IValidator<UpdateShiftDTO> validator, IShiftRepository repository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditShiftCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

            if (!validationOfficeId)
            {
                var error = $"OfficeID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = response.Errors
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var validationShiftId = await _repository.CheckExistShiftId(request.OfficeId, request.DTO.Id);

            if (!validationShiftId)
            {
                var error = $"ID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = response.Errors
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            if (request.DTO.NextDay == false)
            {
                var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = response.Errors
                    });
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }

            try
            {
                var shift = _mapper.Map<Shift>(request.DTO);
                shift.OfficeId = request.OfficeId;

                await _repository.Update(shift);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = shift.Id
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", shift.Id);
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
