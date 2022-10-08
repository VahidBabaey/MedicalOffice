using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MembershipFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MembershipFile.Handlers.Commands
{


    public class EditMembershipCommandHandler : IRequestHandler<EditMembershipCommand, BaseCommandResponse>
    {
        private readonly IMembershipRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditMembershipCommandHandler(IMembershipRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(EditMembershipCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            Log log = new();

            try
            {
                var membership = _mapper.Map<Membership>(request.DTO);

                await _repository.Update(membership);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = membership.Id });
                if (request.DTO.ServiceIDs == null)
                {

                }
                else
                {
                    await _repository.DeleteMembershipIdofServiceAsync(membership.Id);
                    foreach (var serviceid in request.DTO.ServiceIDs)
                    {
                        await _repository.InsertMembershipIdofServiceAsync(serviceid, membership.Id);
                    }
                }
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
