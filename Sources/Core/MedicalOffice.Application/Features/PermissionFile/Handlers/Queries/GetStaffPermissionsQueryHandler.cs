using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
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
    public class GetStaffPermissionsQueryHandler : IRequestHandler<GetStaffPermissionsQuery, BaseResponse>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IUserOfficePermissionRepository _userOfficePermissionRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetStaffPermissionsQueryHandler(
            IMedicalStaffRepository medicalStaffRepository,
            IUserOfficePermissionRepository userOfficePermissionRepository,
            IPermissionRepository permissionRepository,
            IMapper mapper,
            ILogger logger)
        {
            _medicalStaffRepository = medicalStaffRepository;
            _userOfficePermissionRepository = userOfficePermissionRepository;
            _mapper = mapper;
            _logger = logger;
            _permissionRepository = permissionRepository;

            _requestTitle = GetType().Name.Replace("QueryHandler", "");
        }

        public async Task<BaseResponse> Handle(GetStaffPermissionsQuery request, CancellationToken cancellationToken)
        {
            var isStaffExist = _medicalStaffRepository.CheckMedicalStaffExist(request.StaffId, request.OfficeId).Result;

            if (!isStaffExist)
            {
                var error = "staff isn't exist in this office";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return ResponseBuilder.Success(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var permissions = await _userOfficePermissionRepository.GetPermissionsByStaffId(request.StaffId, request.OfficeId);

            var parentIds = permissions.Select(x => x.ParentId).Distinct().ToList();

            var parentPermissions = await _permissionRepository.GetByParentIds(parentIds);

            permissions.AddRange(parentPermissions);

            var result = GetMenu(permissions);

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
