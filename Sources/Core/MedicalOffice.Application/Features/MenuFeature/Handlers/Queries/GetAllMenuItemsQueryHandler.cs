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

        public GetAllMenuItemsQueryHandler(IMenuRepository menuRepository, IUserResolverService userResolver)
        {
            _menuRepository = menuRepository;
            _userResolver = userResolver;
        }

        public async Task<BaseResponse> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userResolver.GetUserId();
            var roleIds = _userResolver.GetUserRoles().Result;

            var x = _menuRepository.GetAllByUserId(userId,request.OfficeId, roleIds);

            return ResponseBuilder.Success(HttpStatusCode.OK, "its Ok", x);
        }
    }
}
