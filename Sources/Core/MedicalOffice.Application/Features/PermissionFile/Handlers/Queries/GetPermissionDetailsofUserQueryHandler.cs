using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Features.PermissionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Handlers.Queries
{

    public class GetPermissionDetailsofMedicalStaffQueryHandler : IRequestHandler<GetPermissionDetailsofMedicalStaff, List<PermissionListDTO>>
    {
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetPermissionDetailsofMedicalStaffQueryHandler(IPermissionRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<List<PermissionListDTO>> Handle(GetPermissionDetailsofMedicalStaff request, CancellationToken cancellationToken)
        {
            List<PermissionListDTO> result = new();

            Log log = new();

            try
            {

                var Permissiondetails = await _repository.GetPermissionDetailsByMedicalStaffID(request.UserOfficeRoleId);

                result = _mapper.Map<List<PermissionListDTO>>(Permissiondetails);

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
