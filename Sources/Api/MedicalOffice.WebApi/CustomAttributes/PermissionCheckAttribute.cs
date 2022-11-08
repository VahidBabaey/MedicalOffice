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
    //public class ClaimRequirementAttribute : TypeFilterAttribute
    //{
    //    public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    //    {
    //        Arguments = new object[] { new Claim(claimType, claimValue) };
    //    }
    //}

    //public class ClaimRequirementFilter : IAuthorizationFilter
    //{
    //    readonly Claim _claim;

    //    public ClaimRequirementFilter(Claim claim

    //        )
    //    {
    //        _claim = claim;
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
    //        if (!hasClaim)
    //        {
    //            context.Result = new ForbidResult();
    //        }
    //    }
    //}

    //[Route("api/resource")]
    //public class MyController : Controller
    //{
    //    [ClaimRequirement("MyClaimTypes.Permission", "CanReadResource")]
    //    [HttpGet]
    //    public IActionResult GetResource()
    //    {
    //        return Ok();
    //    }
    //}

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
            IPermissionRepository repository
            )
        {
            _permission = permission;
            _repository = repository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var officeId = QueryHelpers.ParseQuery(context.HttpContext.Request.QueryString.Value).ToString();
            bool permission = false;

            if (officeId != null)
            {
                permission = _repository.GetByUserAndOfficeId(Guid.Parse(userId), Guid.Parse(officeId)).Result.Any(p => p.Name == _permission);
                if (!permission)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }

    //[Route("api/resource")]
    //public class MyController : Controller
    //{
    //    [PermissionCheck("CanReadResource")]
    //    [HttpGet]
    //    public IActionResult GetResource()
    //    {
    //        return Ok();
    //    }
    //}
}

