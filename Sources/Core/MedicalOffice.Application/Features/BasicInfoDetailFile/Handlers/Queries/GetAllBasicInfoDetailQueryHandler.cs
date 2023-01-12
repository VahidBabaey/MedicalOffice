using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Handlers.Queries
{

    public class GetAllBasicInfoDetailQueryHandler : IRequestHandler<GetAllBasicInfoDetailQuery, List<BasicInfoDetailListDTO>>
    {
        private readonly IBasicInfoDetailRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllBasicInfoDetailQueryHandler(IBasicInfoDetailRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<BasicInfoDetailListDTO>> Handle(GetAllBasicInfoDetailQuery request, CancellationToken cancellationToken)
        {
            List<BasicInfoDetailListDTO> result = new();

            Log log = new();

            try
            {
                var basicInfoDetails = await _repository.GetByBasicInfoId(request.BasicInfoId);

                result = _mapper.Map<List<BasicInfoDetailListDTO>>(basicInfoDetails);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData="در خواندن اطلاعات مشکلی پیش آمده.";
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
