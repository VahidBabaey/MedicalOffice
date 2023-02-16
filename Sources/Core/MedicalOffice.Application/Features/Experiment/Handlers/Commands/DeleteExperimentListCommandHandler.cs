using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class DeleteExperimentListCommandHandler : IRequestHandler<DeleteExperimentListCommand, BaseResponse>
{
    private readonly IExperimentRepository _repository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteExperimentListCommandHandler(IExperimentRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteExperimentListCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        Log log = new();

        foreach (var item in request.DTO.ExperimentId)
        {
            var validationSectionId = await _repository.CheckExistExperimentId(request.OfficeId, item);

            if (!validationSectionId)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add("ID isn't exist");

                log.Type = LogType.Error;
                return response;
            }
        }

        try
        {
            foreach (var item in request.DTO.ExperimentId)
            {
                await _repository.SoftDelete(item);
            }

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (new { Id = request.DTO.ExperimentId });

            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.BadRequest;
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
