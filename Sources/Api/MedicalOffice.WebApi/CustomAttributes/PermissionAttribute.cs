using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MedicalOffice.Application.Contracts.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using MedicalOffice.Application.Constants;
using System.Linq;
using MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands;
using Microsoft.Extensions.Primitives;

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

            //var Roles = context.HttpContext.User.FindAll(ClaimTypes.Role).ToList();

            var OfficeRoles = context.HttpContext.User.Claims.Where(c => c.Type == "OfficeRole").ToList();

            //var OfficeRolesNew = new List<OfficeRole>();

            //foreach (var item in OfficeRoles)
            //{
            //    OfficeRolesNew.Add(new OfficeRole
            //    {
            //        OfficeId = Guid.Parse(item.Value.Split("|")[0]),
            //        RoleId = Guid.Parse(item.Value.Split("|")[1])
            //    });
            //}
            var officeId = QueryHelpers.ParseQuery(context.HttpContext.Request.QueryString.Value)
                    .ToDictionary(x => x.Key, x => x.Value)["officeId"];


            var isSuperAdmin = GetIsRole(OfficeRoles, string.Empty, SuperAdminRole.Id);

            var isAdminRole = GetIsRole(OfficeRoles, officeId, AdminRole.Id);

            if (!isAdminRole && !isSuperAdmin)
            {
                var hasPermission = await _repository.UserHasPermission(Guid.Parse(userId), Guid.Parse(officeId), permissions);
                if (!hasPermission)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
        private static bool GetIsRole(List<Claim> OfficeRoles, StringValues officeId, Guid id)
        {
            return OfficeRoles.Any(x =>
                            x.Value.Split("|")[0] == officeId &&
                            x.Value.Split("|")[1] == id.ToString());
        }
    }
}
