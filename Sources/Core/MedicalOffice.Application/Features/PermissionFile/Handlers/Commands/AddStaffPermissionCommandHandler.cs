using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PermissionFile.Requests.Commands;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PermissionFile.Handlers.Commands
{
    public class AddStaffPermissionCommandHandler : IRequestHandler<AddStaffPermissionsCommand, BaseResponse>
    {
        private readonly IPermissionRepository _PermissionRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IUserOfficePermissionRepository _userOfficePermissionRepository;

        public AddStaffPermissionCommandHandler(IPermissionRepository permissionRepository, ILogger logger, IMapper mapper)
        {
            _PermissionRepository = permissionRepository;
            _logger = logger;
            _mapper = mapper;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddStaffPermissionsCommand request, CancellationToken cancellationToken)
        {
            var medicalStaff = _medicalStaffRepository.GetById(request.StaffId);

            if (medicalStaff == null)
            {
                var error = $"The user didn't registered in this office";

                return ResponseBuilder.Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }

            var existingpermissions = _userOfficePermissionRepository.GetAll().Result.Where(uop => uop.UserId == medicalStaff.Result.UserId && uop.OfficeId == request.OfficeId).ToList();

            throw new NotImplementedException();
        }
    }
}
