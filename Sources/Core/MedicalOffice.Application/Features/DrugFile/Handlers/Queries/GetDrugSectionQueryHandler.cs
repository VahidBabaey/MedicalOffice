﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugD;
using MedicalOffice.Application.Features.DrugFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Handlers.Queries
{

    public class GetDrugSectionQueryHandler : IRequestHandler<GetDrugSectionQuery, List<DrugSectionListDTO>>
    {
        private readonly IDrugSectionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetDrugSectionQueryHandler(IDrugSectionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<DrugSectionListDTO>> Handle(GetDrugSectionQuery request, CancellationToken cancellationToken)
        {
            List<DrugSectionListDTO> result = new();

            Log log = new();

            try
            {
                var drugsection = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);

                result = _mapper.Map<List<DrugSectionListDTO>>(drugsection);

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
