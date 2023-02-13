using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MenuDTO;
using MedicalOffice.Application.Dtos.PermissionDTO;
using MedicalOffice.Application.Features.PermissionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Handlers.Queries
{
    internal class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, BaseResponse>
    {
        private readonly IPermissionRepository _PermissionRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public GetPermissionsQueryHandler(IPermissionRepository permissionRepository, ILogger logger, IMapper mapper)
        {
            _PermissionRepository = permissionRepository;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("QueryHandler","");
        }

        public async Task<BaseResponse> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            var Permissions = _PermissionRepository.GetAll().Result.ToList();
            var result = GetMenu(Permissions);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
        }

        private List<PermissionListDto> GetMenu(List<Permission> Permissions)
        {
            var groupedMenu = Permissions.GroupBy(p => p.ParentId, (key, grpup) => new { ParentId = key, Permissions = grpup.ToList() });

            var parents = new List<PermissionListDto>();
            parents.AddRange(groupedMenu.Where(x => x.ParentId == null).SelectMany(x => x.Permissions).Select(x => _mapper.Map<PermissionListDto>(x)));

            foreach (var item in parents)
            {
                if (groupedMenu.Any(x => x.ParentId != null && (Guid)x.ParentId == item.Id))
                {
                    var children = groupedMenu.Where(x => x.ParentId != null && (Guid)x.ParentId == item.Id)
                        .SelectMany(x => x.Permissions)
                        .Select(x => _mapper.Map<PermissionListDto>(x)).ToList();

                    if (children != null)
                    {
                        item.Children = children;
                    }
                }
            }
            return parents;
        }
    }
}
