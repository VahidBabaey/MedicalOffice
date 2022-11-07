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

namespace MedicalOffice.WebApi.Attributes
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;
        //readonly Guid _officeId;
        //private readonly IUserOfficeRoleRepository _repository;

        public ClaimRequirementFilter(Claim claim
            //Guid officeId,
            //IUserOfficeRoleRepository repository
            )
        {
            //_officeId = officeId;
            _claim = claim;
            //_repository = repository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            //if (userId != null)
            //{
            //    var permissions = await _repository.GetByUserId(_officeId);
            //}

            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            if (!hasClaim)
            {
                context.Result = new ForbidResult();
            }
        }
    }

    [Route("api/resource")]
    public class MyController : Controller
    {
        [ClaimRequirement("MyClaimTypes.Permission", "CanReadResource")]
        [HttpGet]
        public IActionResult GetResource()
        {
            return Ok();
        }
    }
}

