using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.OfficeFeature.Handlers.Queries
{
    public class GetByUserIdQueryHandler : IRequestHandler<GetByUserIdQuery, BaseResponse>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOfficeRepository _officeRepository;
        private readonly string _requestTitle;

        public GetByUserIdQueryHandler(IMapper mapper, ILogger logger, IOfficeRepository repository)
        {
            _mapper = mapper;
            _logger = logger;
            _officeRepository = repository;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var offices = await _officeRepository.GetByUserId(request.UserId);
            if (offices == null)
            {
                var error = $"No office found";
                return await Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }  
            
            var result = _mapper.Map<HashSet<Office?>>(offices);

            return await Success(HttpStatusCode.OK, $"{_requestTitle} succeded",result);
        }

        private async Task<BaseResponse> Success(HttpStatusCode statusCode, string message, params object[] data)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = message,
                AdditionalData = data
            });
            return new() { StatusCode = statusCode, Success = true, StatusDescription = message, Data = data.ToList() };
        }

        private async Task<BaseResponse> Faild(HttpStatusCode statusCode, string message, params string[] errors)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = message,
                AdditionalData = errors
            });
            return new() { StatusCode = statusCode, Success = false, StatusDescription = message, Errors = errors.ToList() };
        }
    }
}