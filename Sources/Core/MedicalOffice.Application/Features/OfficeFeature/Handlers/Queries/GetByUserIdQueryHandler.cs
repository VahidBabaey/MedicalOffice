using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Dtos.OfficeDTO.Validators;
using MedicalOffice.Application.Dtos.PatientDTO.Validators;
using MedicalOffice.Application.Dtos.PatientIllnessFormListDTO;
using MedicalOffice.Application.Features.Office.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Application.Responses.Enveloping;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Office.Handlers.Queries
{
    public class GetByUserIdQueryHandler : IRequestHandler<GetByUserIdQuery, BaseQueryResponse>
    {
        private readonly IOfficeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetByUserIdQueryHandler(IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseQueryResponse> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            OfficesByUserIdValidator validator = new();
            Log log = new();

            var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);

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
                    var offices = await _repository.GetByUserId(new Guid(request.Dto.UserId));
                    //response.Data = _mapper.Map<List<OfficeDTO>>(offices);
                }
                catch (Exception)
                {

                }
            }
            throw new Exception();
        }

    }
}
