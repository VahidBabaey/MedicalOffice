using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Queries
{

    public class GetAllMedicalStaffsNamesandRolesQueryHandler : IRequestHandler<GetAllMedicalStaffsNamesandRolesQuery, List<MedicalStaffNamesDTO>>
    {
        private readonly IMedicalStaffRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllMedicalStaffsNamesandRolesQueryHandler(IMedicalStaffRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<MedicalStaffNamesDTO>> Handle(GetAllMedicalStaffsNamesandRolesQuery request, CancellationToken cancellationToken)
        {
            List<MedicalStaffNamesDTO> result = new();
            Log log = new();

            try
            {
                var MedicalStaffs = await _repository.GetAllMedicalStaffsNamesandRoles();

                result = _mapper.Map<List<MedicalStaffNamesDTO>>(MedicalStaffs);

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
