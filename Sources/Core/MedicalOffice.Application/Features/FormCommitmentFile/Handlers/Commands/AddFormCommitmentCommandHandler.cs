using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
using MedicalOffice.Application.Dtos.FormCommitmentDTO.Validators;
using MedicalOffice.Application.Dtos.InsuranceDTO.Validators;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Handlers.Commands
{
    public class AddFormCommitmentCommandHandler : IRequestHandler<AddFormCommitmentCommand, BaseResponse>
    {
        private readonly IValidator<AddFormCommitmentDTO> _validator;
        private readonly IFormCommitmentRepository _formcommitmentrepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddFormCommitmentCommandHandler(IOfficeRepository officeRepository, IValidator<AddFormCommitmentDTO> validator, IFormCommitmentRepository formcommitmentrepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _validator = validator;
            _formcommitmentrepository = formcommitmentrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddFormCommitmentCommand request, CancellationToken cancellationToken)
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

            var validationFormCommitmentName = await _formcommitmentrepository.CheckExistFormCommitmentName(request.OfficeId, request.DTO.Name);

            if (validationFormCommitmentName)
            {
                var error = "Name Must be Unique";
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
                    var formcommitment = _mapper.Map<FormCommitment>(request.DTO);
                    formcommitment.OfficeId = request.OfficeId;

                    formcommitment = await _formcommitmentrepository.Add(formcommitment);

                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} succeded",
                        AdditionalData = formcommitment.Id
                    });
                    return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", formcommitment.Id);
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
