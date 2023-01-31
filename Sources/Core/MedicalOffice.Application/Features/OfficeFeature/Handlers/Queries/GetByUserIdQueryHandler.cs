using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.OfficeFeature.Handlers.Queries
{
    public class GetByUserIdQueryHandler : IRequestHandler<GetByUserIdQuery, BaseResponse>
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public GetByUserIdQueryHandler(
            IUserResolverService userResolverService,
            IMapper mapper,
            ILogger logger,
            IOfficeRepository repository)
        {
            _userResolverService = userResolverService;
            _mapper = mapper;
            _logger = logger;
            _officeRepository = repository;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userResolverService.GetUserId().Result;
            var result = new List<OfficeListDTO>();

            var offices = await _officeRepository.GetByUserId(Guid.Parse(userId));
            if (offices.Any())
            {
                foreach (var office in offices)
                {
                    result.Add(_mapper.Map<OfficeListDTO>(office));
                }
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
        }
    }
}