using AutoMapper;
using MediatR;
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
    public class GetAllMenuItemsQueryHandler : IRequestHandler<GetMenuQuery, BaseResponse>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public GetAllMenuItemsQueryHandler(
            IMenuRepository menuRepository,
            IUserResolverService userResolver,
            ILogger logger,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var menu = _menuRepository.GetAll().Result.ToList();
            var result = GetMenu(menu);

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
            var groupedMenu = Menu.GroupBy(p => p.ParentId, (key, grpup) => new { ParentId = key, Menus = grpup.ToList() });

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
