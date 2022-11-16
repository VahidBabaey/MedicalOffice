﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugDTO.Validators;
using MedicalOffice.Application.Features.DrugFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugFile.Handlers.Commands
{

    public class EditDrugCommandHandler : IRequestHandler<EditDrugCommand, BaseResponse>
    {
        private readonly IDrugRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditDrugCommandHandler(IDrugRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditDrugCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            try
            {
                var drug = _mapper.Map<Drug>(request.DTO);

                await _repository.Update(drug);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = drug.Id });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
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
