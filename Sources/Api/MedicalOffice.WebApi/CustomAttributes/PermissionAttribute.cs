using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MedicalOffice.Application.Contracts.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using MedicalOffice.Application.Constants;

namespace MedicalOffice.WebApi.Attributes
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(string Permissions) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { Permissions };
        }
    }

    public class PermissionFilter : IAuthorizationFilter
    {
        readonly string _permissions;
        private readonly IPermissionRepository _repository;

        public PermissionFilter(
            string permissions,
            IPermissionRepository repository)
        {
            _permissions = permissions;
            _repository = repository;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            string[] separatingStrings = { ",", " " };
            string[] permissions = _permissions.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);

            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's UserId
            var Roles = context.HttpContext.User.FindAll(ClaimTypes.Role).ToList();
            if (!Roles.Any(x => x.Value == AdminRole.Name))
            {
                var officeId = QueryHelpers.ParseQuery(context.HttpContext.Request.QueryString.Value)

                    .ToDictionary(x => x.Key, x => x.Value)["officeId"];

                var hasPermission = await _repository.UserHasPermission(Guid.Parse(userId), Guid.Parse(officeId), permissions);
                if (!hasPermission)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}

