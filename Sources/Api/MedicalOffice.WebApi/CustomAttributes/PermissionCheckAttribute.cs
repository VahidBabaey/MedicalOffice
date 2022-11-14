using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Application.Contracts.Persistence;
using Microsoft.AspNetCore.WebUtilities;

namespace MedicalOffice.WebApi.Attributes
{
    public class PermissionCheckAttribute : TypeFilterAttribute
    {
        public PermissionCheckAttribute(string Permission) : base(typeof(PermissionFilter))
        {

            Arguments = new object[] { Permission };
        }
    }

    public class PermissionFilter : IAuthorizationFilter
    {
        readonly string _permission;
        private readonly IPermissionRepository _repository;

        public PermissionFilter(
            string permission,
            IPermissionRepository repository)
        {
            _permission = permission;
            _repository = repository;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var officeId = QueryHelpers.ParseQuery(context.HttpContext.Request.QueryString.Value)
                .ToDictionary(x => x.Key, x => x.Value)["officeId"];

            var hasPermission = await _repository.UserHasPermission(Guid.Parse(userId), Guid.Parse(officeId), _permission);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

