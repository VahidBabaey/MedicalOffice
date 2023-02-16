using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Commands
{

    public class DeleteMembershipServiceCommandHandler : IRequestHandler<DeleteMembershipServiceCommand, BaseResponse>
    {
        private readonly IMemberShipServiceRepository _repository;
        private readonly IServiceRepository _servicerepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeleteMembershipServiceCommandHandler(IServiceRepository servicerepository, IMemberShipServiceRepository repository, IMapper mapper, ILogger logger)
        {
            _servicerepository = servicerepository;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeleteMembershipServiceCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();
            Log log = new();

            var validationServiceId = await _repository.CheckExistMemberShipServiceId(request.OfficeId, request.MembershipServiceId);

            if (!validationServiceId)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add("ID isn't exist");

                log.Type = LogType.Error;
                return response;
            }

            try
            {
                await _repository.SoftDelete(request.MembershipServiceId);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = request.MembershipServiceId });

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
}


