using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.SpecializationDTO;
using MedicalOffice.Application.Features.SpecializationFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.SpecializationFile.Handlers.Queries
{

    public class GetAllSpecializationsQueryHandler : IRequestHandler<GetAllSpecializationsQuery, List<SpecializationListDTO>>
    {
        private readonly ISpecializationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllSpecializationsQueryHandler(ISpecializationRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<SpecializationListDTO>> Handle(GetAllSpecializationsQuery request, CancellationToken cancellationToken)
        {
            List<SpecializationListDTO> result = new();

            Log log = new();

            try
            {
                var Section = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);
                //var Section = await _repository.GetAlllist();

                result = _mapper.Map<List<SpecializationListDTO>>(Section);

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
