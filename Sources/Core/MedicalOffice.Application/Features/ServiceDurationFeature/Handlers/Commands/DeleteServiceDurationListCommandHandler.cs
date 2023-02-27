using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceDurationFeature.Handlers.Commands
{
    public class DeleteServiceDurationListCommandHandler : IRequestHandler<DeleteServiceDurationListCommand, BaseResponse>
    {
        private readonly IServiceDurationRepositopry _serviceDuratoinRepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeleteServiceDurationListCommandHandler
            (IServiceDurationRepositopry serviceDuratoinRepository,
            ILogger logger)
        {
            _serviceDuratoinRepository = serviceDuratoinRepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler",string.Empty);  
        }

        public async Task<BaseResponse> Handle(DeleteServiceDurationListCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _serviceDuratoinRepository.DeleteRange(request.DTO.ServiceDurationIds);

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = request.DTO.ServiceDurationIds
                });

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", request.DTO.ServiceDurationIds);

            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error.Message
                });

                return ResponseBuilder.Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", error.Message);
            }
        }
    }
}
