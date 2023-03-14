using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.OfficeFeature.Handlers.Queries
{
    public class GetByOfficeIdQueryHandler : IRequestHandler<GetByOfficeIdQuery, BaseResponse>
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public GetByOfficeIdQueryHandler(
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

        public async Task<BaseResponse> Handle(GetByOfficeIdQuery request, CancellationToken cancellationToken)
        {
            var office = await _officeRepository.GetById(request.officeId);
            if (office == null)
            {
                var error = "مطب با این شناسه وجود ندارد.";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var result = _mapper.Map<OfficeListDTO>(office);
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