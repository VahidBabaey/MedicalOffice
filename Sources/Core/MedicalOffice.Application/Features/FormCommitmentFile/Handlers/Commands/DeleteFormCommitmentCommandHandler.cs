using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.FormCommitmentFile.Requests.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.FormCommitmentFile.Handlers.Commands
{
    public class DeleteFormCommitmentCommandHandler : IRequestHandler<DeleteFormCommitmentCommand, BaseResponse>
    {
        private readonly IFormCommitmentRepository _repository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeleteFormCommitmentCommandHandler(IFormCommitmentRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeleteFormCommitmentCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            var validationFormCommitmentId = await _repository.CheckExistFormCommitmentId(request.OfficeId, request.FormCommitmentID);

            if (!validationFormCommitmentId)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add("ID isn't exist");

                log.Type = LogType.Error;
                return response;
            }

            try
            {
                await _repository.Delete(request.FormCommitmentID);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = request.FormCommitmentID });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
