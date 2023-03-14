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

public class DeleteShiftListCommandHandler : IRequestHandler<DeleteShiftListCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IShiftRepository _shiftrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteShiftListCommandHandler(IOfficeRepository officeRepository, IShiftRepository shiftrepository, ILogger logger)
    {
        _officeRepository = officeRepository;
        _shiftrepository = shiftrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }
    public async Task<BaseResponse> Handle(DeleteShiftListCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.DTO.ShiftId)
        {
            var validationSectionId = await _shiftrepository.CheckExistShiftId(request.OfficeId, item);

            if (!validationSectionId)
            {
                var error = "شناسه شیفت وجود ندارد.";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }
        }

        try
        {
            foreach (var item in request.DTO.ShiftId)
            {
                await _shiftrepository.SoftDelete(item);
            }

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
