using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.FormCommitmentDTO;
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
    public class EditFormCommitmentCommandHandler : IRequestHandler<EditFormCommitmentCommand, BaseResponse>
    {
        private readonly IFormCommitmentRepository _formcommitmentrepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditFormCommitmentCommandHandler(IOfficeRepository officeRepository, IFormCommitmentRepository formcommitmentrepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _formcommitmentrepository = formcommitmentrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditFormCommitmentCommand request, CancellationToken cancellationToken)
        {

            var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

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

            var validationFormCommitmentId = await _formcommitmentrepository.CheckExistFormCommitmentId(request.OfficeId, request.DTO.Id);

            if (!validationFormCommitmentId)
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
            else
            {
                try
                {
                    var formcommitment = _mapper.Map<FormCommitment>(request.DTO);
                    formcommitment.OfficeId = request.OfficeId;

                    await _formcommitmentrepository.Update(formcommitment);

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
