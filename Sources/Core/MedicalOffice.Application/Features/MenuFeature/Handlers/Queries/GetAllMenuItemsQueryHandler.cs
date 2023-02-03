using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MenuFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MenuFeature.Handlers.Queries
{
    public class GetAllMenuItemsQueryHandler : IRequestHandler<GetAllMenuItemsQuery, BaseResponse>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserResolverService _userResolver;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly ILogger _logger;

        private readonly string _requestTitle;

        public GetAllMenuItemsQueryHandler(
            IMenuRepository menuRepository,
            IUserResolverService userResolver,
            IRoleRepository roleRepository,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            ILogger logger)
        {
            _menuRepository = menuRepository;
            _userResolver = userResolver;
            _roleRepository = roleRepository;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userResolver.GetUserId();
            var roleNames = _userResolver.GetUserRoles().Result;

            var allItemValidRoles = new[] { AdminRole.Id, SuperAdminRole.Id };
            var isUserAdminOrSuperAdmin = _userOfficeRoleRepository
                .GetByUserAndOfficeId(Guid.Parse(userId), request.OfficeId).Result
                .Any(x => allItemValidRoles.Contains(x.RoleId));

            if (isUserAdminOrSuperAdmin)
            {
                var result =await _menuRepository.GetAll();

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = result
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
            }

            var roleIs = _roleRepository.GetAll().Result.Where(x => roleNames.Contains(x.Name)).Select(x => x.Id).ToList();

            var x = await _menuRepository.GetAllByUserId(Guid.Parse(userId), request.OfficeId, roleIs);

            return ResponseBuilder.Success(HttpStatusCode.OK, "its Ok", x);
        }
    }
}
