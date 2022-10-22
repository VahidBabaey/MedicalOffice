using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.DrugIntractionDTO;
using MedicalOffice.Application.Features.DrugIntractionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.DrugIntractionFile.Handlers.Queries
{

    public class GetAllDrugIntractionQueryHandler : IRequestHandler<GetAllDrugIntraction, List<DrugIntractionListDTO>>
    {
        private readonly IDrugIntractionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllDrugIntractionQueryHandler(IDrugIntractionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<DrugIntractionListDTO>> Handle(GetAllDrugIntraction request, CancellationToken cancellationToken)
        {
            List<DrugIntractionListDTO> result = new();

            Log log = new();

            try
            {
                var drugintraction = await _repository.GetAllDrugIntractions();

                result = _mapper.Map<List<DrugIntractionListDTO>>(drugintraction);

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
