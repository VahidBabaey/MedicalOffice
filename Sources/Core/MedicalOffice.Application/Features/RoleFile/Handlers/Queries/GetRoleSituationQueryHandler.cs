using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.RoleDTO;
using MedicalOffice.Application.Features.RoleFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
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

        public GetRoleSituationQueryHandler(IRoleRepository roleRepository, ILogger logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetRoleSituationQuery request, CancellationToken cancellationToken)
        {
            var roleSituation = new RoleSituationDTO();
            var role = _roleRepository.GetById(request.RoleId).Result;

            if (role != null)
            {
                if (role.Id == DoctorRole.Id)
                {
                    roleSituation.IsSpecialization = true;
                    roleSituation.IsTechnicalAssistant = true;
                    roleSituation.Specialty = true;
                }

                if (role.Id == ExpertRole.Id)
                {
                    roleSituation.WorkingField = true;
                }

                if (role.Id == SecretaryRole.Id)
                {
                    roleSituation.IsActive = false;
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
