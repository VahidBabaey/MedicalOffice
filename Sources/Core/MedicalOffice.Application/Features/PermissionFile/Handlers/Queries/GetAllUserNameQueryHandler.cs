﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.UserDTO;
using MedicalOffice.Application.Features.PermissionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Handlers.Queries
{

    public class GetAllUserNameQueryHandler : IRequestHandler<GetAllUsersName, List<UserNameListDTO>>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllUserNameQueryHandler(IUserRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<UserNameListDTO>> Handle(GetAllUsersName request, CancellationToken cancellationToken)
        {
            List<UserNameListDTO> result = new();

            Log log = new();

            try
            {
                var Users = await _repository.GetAllUsersName();

                result = _mapper.Map<List<UserNameListDTO>>(Users);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.Messages.Add(error.Message);
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
