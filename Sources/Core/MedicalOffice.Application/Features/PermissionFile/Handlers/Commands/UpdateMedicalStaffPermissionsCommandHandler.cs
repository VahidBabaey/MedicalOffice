using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PermissionFile.Requests.Commands;
using MedicalOffice.Application.Models;
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
    public class UpdateMedicalStaffPermissionsCommandHandler : IRequestHandler<UpdateMedicalStaffCommand, BaseResponse>
    {
        private readonly IMedicalStaffPermissionRepository _medicalStaffPermissionRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public UpdateMedicalStaffPermissionsCommandHandler(
            ILogger loggre,
            IMedicalStaffRepository medicalStaffRepository,
            IPermissionRepository permissionRepository,
            IMedicalStaffPermissionRepository medicalStaffPermissionRepository
            )
        {
            _logger = loggre;
            _medicalStaffRepository = medicalStaffRepository;
            _permissionRepository = permissionRepository;
            _medicalStaffPermissionRepository = medicalStaffPermissionRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(UpdateMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            var medicalStaff = _medicalStaffRepository.GetByID(request.DTO.MedicalStaffId);

            if (medicalStaff == null)
            {
                var error = $"The user didn't registered in this office";
                return await Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }

            var existingpermissions = _medicalStaffPermissionRepository.GetAll().Result.Where(mp => mp.MedicalStaffId == medicalStaff.Id);

            var newPermissionIds = _permissionRepository.GetAll().Result.Where(m => request.DTO.PermissionIds.Contains(m.Id))
                .Select(p => p.Id);

            var medicalStaffPermissions = new List<MedicalStaffPermission>();

            foreach (var permissionId in newPermissionIds)
            {
                medicalStaffPermissions.Add(new MedicalStaffPermission
                {
                    MedicalStaffId = medicalStaff.Id,
                    PermissionId = permissionId
                });
            };

            if (existingpermissions != null)
            {
                foreach (var permission in existingpermissions)
                {
                    await _medicalStaffPermissionRepository.Delete(permission);
                }
            };

            foreach (var permissionId in newPermissionIds)
            {
                await _medicalStaffPermissionRepository.Add(new MedicalStaffPermission
                {
                    MedicalStaffId = medicalStaff.Id,
                    PermissionId = permissionId
                });
            };

            return await Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", new
            {
                medicalStaff
                .Id
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
