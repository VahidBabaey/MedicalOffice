﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Queries
{
    public class GetAllMemberShipsNamesQueryHandler : IRequestHandler<GetAllMemberShipsNamesQuery, List<MembershipNamesDTO>>
    {
        private readonly IReceptionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllMemberShipsNamesQueryHandler(IReceptionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<MembershipNamesDTO>> Handle(GetAllMemberShipsNamesQuery request, CancellationToken cancellationToken)
        {
            List<MembershipNamesDTO> result = new();

            Log log = new();

            try
            {
                var membershipNames = await _repository.GetAllMembershipNames();

                result = _mapper.Map<List<MembershipNamesDTO>>(membershipNames);

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
