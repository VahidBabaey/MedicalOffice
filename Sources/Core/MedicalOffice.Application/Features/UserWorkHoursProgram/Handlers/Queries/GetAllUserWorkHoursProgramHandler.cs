using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO;
using MedicalOffice.Application.Features.UserWorkHoursProgram.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserWorkHoursProgram.Handlers.Queries
{

    public class GetAllUserWorkHoursProgramHandler : IRequestHandler<GetAllUserWorkHoursQuery, List<UserWorkHoursProgramListDTO>>
    {
        private readonly IUserWorkHourProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllUserWorkHoursProgramHandler(IUserWorkHourProgramRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<UserWorkHoursProgramListDTO>> Handle(GetAllUserWorkHoursQuery request, CancellationToken cancellationToken)
        {
            List<UserWorkHoursProgramListDTO> result = new();

            Log log = new();

            try
            {
                var Userworkhoursprograms = await _repository.GetUserWorkHourProgramByID(request.UserId);

                result = _mapper.Map<List<UserWorkHoursProgramListDTO>>(Userworkhoursprograms);

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
