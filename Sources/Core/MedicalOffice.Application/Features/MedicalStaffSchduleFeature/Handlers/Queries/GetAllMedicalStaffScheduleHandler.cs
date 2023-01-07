using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Handlers.Queries
{

    public class GetAllMedicalStaffScheduleHandler : IRequestHandler<GetAllMedicalStaffScheduleQuery, List<MedicalStaffScheduleListDTO>>
    {
        private readonly IMedicalStaffScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllMedicalStaffScheduleHandler(IMedicalStaffScheduleRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<MedicalStaffScheduleListDTO>> Handle(GetAllMedicalStaffScheduleQuery request, CancellationToken cancellationToken)
        {
            List<MedicalStaffScheduleListDTO> result = new();

            Log log = new();

            try
            {
                var MedicalStaffworkhoursprograms = await _repository.GetMedicalStaffScheduleById(request.MedicalStaffId);

                result = _mapper.Map<List<MedicalStaffScheduleListDTO>>(MedicalStaffworkhoursprograms);

                log.Header = $"{_requestTitle} succeded";
                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                log.Header = $"{_requestTitle} failed";
                log.AdditionalData=error.Message;
                log.Type = LogType.Error;
            }

            await _logger.Log(log);

            return result;
        }
    }

}
