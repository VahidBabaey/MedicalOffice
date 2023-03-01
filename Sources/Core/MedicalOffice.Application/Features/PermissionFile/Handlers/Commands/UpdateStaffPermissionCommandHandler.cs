using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PermissionFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Handlers.Commands
{
    public class UpdateStaffPermissionCommandHandler : IRequestHandler<UpdateStaffPermissionsCommand, BaseResponse>
    {
        private readonly IPermissionRepository _PermissionRepository;
        private readonly ILogger _logger;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IUserOfficePermissionRepository _userOfficePermissionRepository;
        private readonly string _requestTitle;

        public UpdateStaffPermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IUserOfficePermissionRepository userOfficePermissionRepository,
            ILogger logger)
        {
            _PermissionRepository = permissionRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _userOfficePermissionRepository = userOfficePermissionRepository;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(UpdateStaffPermissionsCommand request, CancellationToken cancellationToken)
        {
            #region checkStaffExist
            var medicalStaff = await _medicalStaffRepository.GetById(request.DTO.staffId);
            if (medicalStaff == null || medicalStaff.OfficeId != request.OfficeId)
            {
                var error = $"The user isn't exist in this office";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }
            #endregion

            #region CheckAllInputsExist
            var isThereAnyWrongPermission = _PermissionRepository.GetAll().Result.All(x => !request.DTO.permissionIds.Contains(x.Id));
            if (isThereAnyWrongPermission)
            {
                var error = $"There is a wrong permission in permissionIds array";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }
            #endregion

            #region RemoveOldStaffPermissions
            await _userOfficePermissionRepository.SoftDeleteRange(request.OfficeId, medicalStaff.UserId);
            #endregion

            #region AddNewPermissions
            var UserOfficePermissions = new List<UserOfficePermission>();
            foreach (var permissionId in request.DTO.permissionIds)
            {
                UserOfficePermissions.Add(new UserOfficePermission
                {
                    UserId = medicalStaff.UserId,
                    OfficeId = request.OfficeId,
                    PermissionId = permissionId
                });
            }
            var newPermissions = await _userOfficePermissionRepository.AddRange(UserOfficePermissions);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = newPermissions.Select(x => x.PermissionId).ToArray()
            });
            return ResponseBuilder.Success(HttpStatusCode.Created, $"{_requestTitle} Succeeded", newPermissions.Select(x => x.PermissionId).ToArray());
            #endregion
        }
    }
}
