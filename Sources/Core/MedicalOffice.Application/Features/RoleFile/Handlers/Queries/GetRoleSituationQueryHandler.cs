using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.RoleDTO;
using MedicalOffice.Application.Features.RoleFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.RoleFile.Handlers.Queries
{
    public class GetRoleSituationQueryHandler : IRequestHandler<GetRoleSituationQuery, BaseResponse>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetRoleSituationQueryHandler(IRoleRepository roleRepository,ILogger logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("QueryHandler",string.Empty);
        }

        public async Task<BaseResponse> Handle(GetRoleSituationQuery request, CancellationToken cancellationToken)
        {
            var roleSituation = new RoleSituationDTO();
            var role = _roleRepository.GetById(request.RoleId).Result;

            Guid[] validRolesForTehnicalAssistantActivation = { DoctorRole.Id, SpecialistRole.Id };
            Guid[] validRolesForSpecializationActivation = { DoctorRole.Id, SpecialistRole.Id };

            if (role != null)
            {
                if (validRolesForTehnicalAssistantActivation.Contains(role.Id))
                {
                    roleSituation.TechnicalAssistant = true;
                }
                if (validRolesForSpecializationActivation.Contains(role.Id))
                {
                    roleSituation.Specialization = true;
                }
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = roleSituation
            });

            return ResponseBuilder.Success(System.Net.HttpStatusCode.OK, $"{_requestTitle} succeeded", roleSituation);
        }
    }
}
