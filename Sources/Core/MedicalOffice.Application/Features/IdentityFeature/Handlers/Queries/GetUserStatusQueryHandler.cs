using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO.Validators;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.WebApi.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Queries
{
    public class GetUserStatusQueryHandler : IRequestHandler<GetUserStatusQuery, BaseResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetUserStatusQueryHandler(ILogger logger, UserManager<User> userManager)
        {
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _userManager = userManager;
        }

        public async Task<BaseResponse> Handle(GetUserStatusQuery request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();
            PhoneNumberValidator validator = new();
            Log log = new();

            var validationResult = await validator.ValidateAsync(request.Dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                try
                {
                    UserStatusDTO userStatus = new();
                    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == request.Dto.PhoneNumber);

                    if (user == null)
                    {
                        userStatus.Exist = false;
                        userStatus.PasswordOption = false;
                    }
                    else
                    {
                        userStatus.PasswordOption = user.PasswordHash == string.Empty ? false : true;

                        var isLockout = await _userManager.IsLockedOutAsync(user);
                        if (isLockout)
                        {
                            userStatus.OtpOption = false;
                            userStatus.PasswordOption = false;
                        }

                        response.Success = true;
                        response.StatusDescription = $"{_requestTitle} succeded";
                        response.Data = (userStatus);

                        log.Type = LogType.Success;
                    }
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.StatusDescription = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
