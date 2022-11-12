using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.OfficeFeature.Handlers.Queries
{
    public class GetByUserIdQueryHandler : IRequestHandler<GetByUserIdQuery, BaseResponse>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IOfficeRepository _repository;
        private readonly string _requestTitle;

        public GetByUserIdQueryHandler(IMapper mapper, ILogger logger, IOfficeRepository repository)
        {
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _repository = repository;
        }

        public async Task<BaseResponse> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();
            Log log = new();

            var offices = await _repository.GetByUserId(request.UserId);
            var result = _mapper.Map<List<Office>>(offices);

            response.Success = true;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (result);

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;

            await _logger.Log(log);

            return response;
        }
    }
}