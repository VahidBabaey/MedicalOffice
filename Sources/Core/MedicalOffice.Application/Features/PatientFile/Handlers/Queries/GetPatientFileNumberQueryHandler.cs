using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Queries
{
    public class GetPatientFileNumberQueryHandler : IRequestHandler<GetPatientFileNumberQuery, BaseResponse>
    {
        private readonly IPatientRepository _patientrepository;
        private readonly ILogger _logger;

        public GetPatientFileNumberQueryHandler(IPatientRepository patientrepository, ILogger logger)
        {
            _logger = logger;
            _patientrepository = patientrepository;
        }

        public async Task<BaseResponse> Handle(GetPatientFileNumberQuery request, CancellationToken cancellationToken)
        {
            var FileNumber = await _patientrepository.GenerateFileNumber(request.OfficeId);
            var _requestTitle = ResponseBuilder.CreateRequestTitle<GetPatientFileNumberQueryHandler>("QueryHandler");

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = FileNumber
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", FileNumber);
        }
    }
}
