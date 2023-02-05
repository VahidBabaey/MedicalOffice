using AutoMapper;
using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MenuDTO;
using MedicalOffice.Application.Features.MenuFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
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
        private readonly IMapper _mapper;

        public GetAllMenuItemsQueryHandler(
            IMenuRepository menuRepository,
            IUserResolverService userResolver,
            IRoleRepository roleRepository,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            ILogger logger,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _userResolver = userResolver;
            _roleRepository = roleRepository;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userResolver.GetUserId();
            var officeRoles = _userResolver.GetOfficeRoles().Result;

            var isUserAdminOrSuperAdmin = officeRoles.Any(x =>
                (x.OfficeId == request.OfficeId && x.RoleId == AdminRole.Id) ||
                x.RoleId == SuperAdminRole.Id);

            if (isUserAdminOrSuperAdmin)
            {
                var menuItems = await _menuRepository.GetAll();

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = MenuItems(menuItems)
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", MenuItems(menuItems));
            }

            var roleIs = officeRoles.Select(x => x.RoleId).ToList();

            var menuItemsByUserId = await _menuRepository.GetAllByUserId(Guid.Parse(userId), request.OfficeId, roleIs);

            return ResponseBuilder.Success(HttpStatusCode.OK, "its Ok", MenuItems(menuItemsByUserId));
        }

        private List<MenuDto> MenuItems(IReadOnlyList<Menu> menu)
        {
            var menuGroups = menu.GroupBy(
                p => p.ParentId,
                (key, g) => new { ParentId = key, Menus = g.ToList() });

            //var menuGroups = menu.OrderBy(a => a.ParentId).GroupBy(x => x.ParentId);
            var menuItems = new List<MenuDto>();
            var parent = new MenuDto();

            foreach (var group in menuGroups)
            {
                if (group.ParentId == null)
                {
                    foreach (var item in group.Menus)
                    {
                        parent.ParentId = group.ParentId;
                        parent.MenuId = item.Id;
                        parent.Name = item.Name;
                        parent.Link = item.Link;

                        menuItems.Add(parent);
                    }
                }
            }

            //foreach (var item in menuItems)
            //{
            //    if (menuGroups.Any(x => x.ParentId != null && (Guid)x.ParentId == item.MenuId))
            //    {
            //        item.children = menuGroups.First(x => x.ParentId != null && (Guid)x.ParentId == item.MenuId).Menus;
            //    }
            //    else
            //    {
            //        item.children = null;
            //    }
            //}

            return menuItems;
        }
    }
}