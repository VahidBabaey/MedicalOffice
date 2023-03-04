using AutoMapper;
using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MenuDTO;
using MedicalOffice.Application.Features.MenuFeature.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.MenuFeature.Handlers.Queries
{
    public class GetMenuByUserQueryHandler : IRequestHandler<GetMenuByUserQuery, BaseResponse>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUserResolverService _userResolver;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public GetMenuByUserQueryHandler(
            IMenuRepository menuRepository,
            IUserResolverService userResolver,
            ILogger logger,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _userResolver = userResolver;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetMenuByUserQuery request, CancellationToken cancellationToken)
        {
            var result = new List<MenuDto>();
            var userId = await _userResolver.GetUserId();
            var officeRoles = _userResolver.GetOfficeRoles().Result;

            var isUserAdminOrSuperAdmin = officeRoles.Any(x =>
                (x.OfficeId == request.OfficeId && x.RoleId == AdminRole.Id) ||
                x.RoleId == SuperAdminRole.Id);

            if (isUserAdminOrSuperAdmin)
            {
                var adminMenu = _menuRepository.GetAll().Result.ToList();
                result = GetMenu(adminMenu);
            }
            else
            {
                var roleIs = officeRoles.Select(x => x.RoleId).ToList();

                var menuByUserId = await _menuRepository.GetAllByUserId(Guid.Parse(userId), request.OfficeId, roleIs);
                result = GetMenu(menuByUserId);
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
        }


        private List<MenuDto> GetMenu(List<Menu> Menu)
        {
            var groupedMenu = Menu.OrderBy(x=>x.Order).GroupBy(p => p.ParentId, (key, grpup) => new { ParentId = key, Menus = grpup.ToList() });

            var parents = new List<MenuDto>();
            parents.AddRange(groupedMenu.Where(x => x.ParentId == null).SelectMany(x => x.Menus).Select(x => _mapper.Map<MenuDto>(x)));

            foreach (var item in parents)
            {
                if (groupedMenu.Any(x => x.ParentId != null && (Guid)x.ParentId == item.Id))
                {
                    var children = groupedMenu.Where(x => x.ParentId != null && (Guid)x.ParentId == item.Id)
                        .SelectMany(x => x.Menus)
                        .Select(x => _mapper.Map<MenuDto>(x)).ToList();

                    if (children != null)
                    {
                        item.children = children;
                    }
                }
            }

            return parents;
        }
    }
}