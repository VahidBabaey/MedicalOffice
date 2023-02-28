using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands;
using MedicalOffice.Application.Features.FormReferalFile.Requests.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormReferalFile.Handlers.Commands
{
    public class DeleteFormReferalCommandHandler : IRequestHandler<DeleteFormReferalCommand, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IFormReferalRepository _formreferalrepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeleteFormReferalCommandHandler(IOfficeRepository officeRepository, IFormReferalRepository formreferalrepository, ILogger logger)
        {
            _officeRepository = officeRepository;
            _formreferalrepository = formreferalrepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeleteFormReferalCommand request, CancellationToken cancellationToken)
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

            var validationFormReferalId = await _formreferalrepository.CheckExistFormReferalId(request.OfficeId, request.FormReferalID);

            if (!validationFormReferalId)
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

            try
            {
                await _formreferalrepository.SoftDelete(request.FormReferalID);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded");
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
