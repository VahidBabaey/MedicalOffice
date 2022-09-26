using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile;
using MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Handlers.Queries
{

    public class GetAllMedicalStaffWorkHoursProgramHandler : IRequestHandler<GetAllMedicalStaffWorkHoursQuery, List<MedicalStaffWorkHoursProgramListDTO>>
    {
        private readonly IMedicalStaffWorkHourProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllMedicalStaffWorkHoursProgramHandler(IMedicalStaffWorkHourProgramRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<MedicalStaffWorkHoursProgramListDTO>> Handle(GetAllMedicalStaffWorkHoursQuery request, CancellationToken cancellationToken)
        {
            List<MedicalStaffWorkHoursProgramListDTO> result = new();

            Log log = new();

            try
            {
                var medicalstaffworkhoursprograms = await _repository.GetMedicalStaffWorkHourProgramByID(request.MedicalStaffId);

                result = _mapper.Map<List<MedicalStaffWorkHoursProgramListDTO>>(medicalstaffworkhoursprograms);

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
