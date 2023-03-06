using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{

    public class DeleteMedicalStaffCommandHandler : IRequestHandler<DeleteMedicalStaffCommand, BaseResponse>
    {
        private readonly IMedicalStaffRepository _medicanlStaffRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeleteMedicalStaffCommandHandler(IMedicalStaffRepository medicanlStaffRepository, IUserOfficeRoleRepository userOfficeRoleRepository, IMapper mapper, ILogger logger)
        {
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _medicanlStaffRepository = medicanlStaffRepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeleteMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            var medicalStaff = await _medicanlStaffRepository.GetExistingStaffById(request.MedicalStaffId, request.OfficeId);

            if (medicalStaff == null)
            {
                var error = $"The medicalStaff isn't exist";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            await _userOfficeRoleRepository.DeleteUserOfficeRoleAsync(medicalStaff.UserId,request.OfficeId);
            await _medicanlStaffRepository.SoftDelete(request.MedicalStaffId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = medicalStaff.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", medicalStaff.Id);
        }
    }
}
