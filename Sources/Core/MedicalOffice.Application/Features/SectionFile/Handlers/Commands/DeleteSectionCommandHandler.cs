using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly ISectionRepository _sectionrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteSectionCommandHandler(IOfficeRepository officeRepository, ISectionRepository sectionrepository, ILogger logger)
    {
        _officeRepository = officeRepository;
        _sectionrepository = sectionrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        var validationSectionId = await _sectionrepository.CheckExistSectionId(request.OfficeId, request.SectionId);

        if (!validationSectionId)
        {
            var error = "شناسه بخش وجود ندارد.";
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
            await _sectionrepository.SoftDelete(request.SectionId);

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
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
        }
    }
}
