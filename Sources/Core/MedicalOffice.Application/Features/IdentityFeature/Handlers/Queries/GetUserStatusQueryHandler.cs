using AutoMapper;
using FluentValidation;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Queries
{
    public class GetUserStatusQueryHandler : IRequestHandler<GetUserStatusQuery, BaseResponse>
    {
        private readonly IValidator<GetByPhoneNumberDTO> _validator;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetUserStatusQueryHandler(
            IValidator<GetByPhoneNumberDTO> validator,
            ILogger logger,
            UserManager<User> userManager)
        {
            _validator = validator;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _userManager = userManager;
        }

        public async Task<BaseResponse> Handle(GetUserStatusQuery request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();
            GetByPhoneNumberValidator validator = new();
            Log log = new();

            var responseBuilder = new ResponseBuilder();

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            UserStatusDTO userStatus = new();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == request.DTO.PhoneNumber);

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
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = userStatus
            });

            return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeede", userStatus);
        }
    }
}
