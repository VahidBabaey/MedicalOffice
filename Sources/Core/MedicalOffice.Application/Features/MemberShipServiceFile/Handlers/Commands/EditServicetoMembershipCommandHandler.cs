using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO.Validators;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Handlers.Commands
{
    public class EditServicetoMembershipCommandHandler : IRequestHandler<EditServicetoMembershipCommand, BaseCommandResponse>
    {
        private readonly IMemberShipServiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditServicetoMembershipCommandHandler(IMemberShipServiceRepository repository, IMapper mapper, ILogger logger)
        {

            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);

        }

        public async Task<BaseCommandResponse> Handle(EditServicetoMembershipCommand request, CancellationToken cancellationToken)
        {

            BaseCommandResponse response = new();

            Log log = new();

            try
            {
                if (request.DTO.ServiceId == null)
                {

                }
                else
                {
                    foreach (var srvid in request.DTO.ServiceId)
                    {
                        await _repository.UpdateServiceOfMemberShipAsync(request.DTO.Discount, srvid, request.DTO.MembershipId);
                    }
                }

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
