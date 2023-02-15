using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PictureFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PictureFile.Handlers.Commands;
public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand, BaseResponse>
{
    private readonly IPictureRepository _repository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeletePictureCommandHandler(IPictureRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeletePictureCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();
        Log log = new();

        try
        {
            await _repository.Delete(request.PictureId);

            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (new { Id = request.PictureId });

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
