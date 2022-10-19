using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.UserDTO.Validators;
using MedicalOffice.Application.Features.UserFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserFile.Handler.Commands
{

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, BaseCommandResponse>
    {
        private readonly IUserRepository _repository;
        private readonly ICryptoServiceProvider _cryptoServiceProvider;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddUserCommandHandler(IUserRepository repository, IMapper mapper, ILogger logger, ICryptoServiceProvider cryptoServiceProvider)
        {
            _repository = repository;
            _cryptoServiceProvider = cryptoServiceProvider;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            AddUserValidator validator = new();

            Log log = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;

                
            }
            else
            {
                try
                {
                    var passwordHash = await _cryptoServiceProvider.GetHash(request.DTO.PasswordHash);
                    var User = _mapper.Map<User>(request.DTO);
                    User.PasswordHash = passwordHash;
                    User = await _repository.Add(User);


                    response.Success = true;
                    response.Message = $"{_requestTitle} succeded";
                    response.Data.Add(new { Id = User.Id });

                    if (request.DTO.RoleIds == null)
                    {

                    }
                    else
                    {
                        foreach (var roleid in request.DTO.RoleIds)
                        {
                            await _repository.InsertToUserOfficeRole(roleid, User.Id);
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
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
