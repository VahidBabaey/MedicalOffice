using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{
    public class UpdateMedicalStaffPermissionsCommandHandler : IRequestHandler<UpdateMedicalStaffCommand, BaseResponse>
    {
        private readonly IUserOfficePermissionRepository _userOfficePermissionRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public UpdateMedicalStaffPermissionsCommandHandler(
            ILogger logger,
            IMedicalStaffRepository medicalStaffRepository,
            IPermissionRepository permissionRepository,
            IUserOfficePermissionRepository userOfficePermissionRepository
            )
        {
            _logger = logger;
            _medicalStaffRepository = medicalStaffRepository;
            _permissionRepository = permissionRepository;
            _userOfficePermissionRepository = userOfficePermissionRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(UpdateMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            var medicalStaff = _medicalStaffRepository.GetById(request.DTO.MedicalStaffId);

            if (medicalStaff == null)
            {
                var error = $"The user didn't registered in this office";
                return await Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }
            var existingpermissions = _userOfficePermissionRepository.GetAll().Result.Where(uop => uop.UserId == medicalStaff.Result.UserId && uop.OfficeId == request.OffceId).ToList();

            var newPermissionIds = _permissionRepository.GetAll().Result.Where(m => request.DTO.PermissionIds.Contains(m.Id))
                .Select(p => p.Id);

            var userOfficePermissions = new List<UserOfficePermission>();

            foreach (var permissionId in newPermissionIds)
            {
                userOfficePermissions.Add(new UserOfficePermission
                {
                    OfficeId = request.OffceId,
                    UserId = medicalStaff.Result.UserId,
                    PermissionId = permissionId
                });
            };

            await _userOfficePermissionRepository.AddUserOfficePermissions(existingpermissions, userOfficePermissions);

            return await Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", new
            {
                medicalStaff.Id
            });
        }

        private async Task<BaseResponse> Success(HttpStatusCode statusCode, string message, params object[] data)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = message,
                AdditionalData = data
            });
            return new() { StatusCode = statusCode, Success = true, StatusDescription = message, Data = data.ToList() };
        }

        private async Task<BaseResponse> Faild(HttpStatusCode statusCode, string message, params string[] errors)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = message,
                AdditionalData = errors
            });
            return new() { StatusCode = statusCode, Success = false, StatusDescription = message, Errors = errors.ToList() };
        }
    }
}
