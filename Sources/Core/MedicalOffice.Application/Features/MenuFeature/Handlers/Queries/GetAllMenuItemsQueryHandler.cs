using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MenuFeature.Requests.Queries;
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

        public GetAllMenuItemsQueryHandler(IMenuRepository menuRepository, IUserResolverService userResolver,IRoleRepository roleRepository)
        {
            _menuRepository = menuRepository;
            _userResolver = userResolver;
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userResolver.GetUserId();
            var roleNames = _userResolver.GetUserRoles().Result;
            var roleIs = _roleRepository.GetAll().Result.Where(x => roleNames.Contains(x.Name)).Select(x=>x.Id).ToList();

            var x = _menuRepository.GetAllByUserId(Guid.Parse(userId),request.OfficeId, roleIs);

            return ResponseBuilder.Success(HttpStatusCode.OK, "its Ok", x);
        }
    }
}
