using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Commands;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Queries;
using MedicalOffice.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator, IUserResolverService userResolverService)
        {
            _userResolverService = userResolverService;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<OfficeListDTO>>> GetByUserId()
        {
            var userId = _userResolverService.GetUserId().Result;

            var response = await _mediator.Send(new GetByUserIdQuery { UserId = Guid.Parse(userId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddOffice([FromBody] OfficeDTO dto)
        {
            var userId = Guid.Parse(_userResolverService.GetUserId().Result);
            var roles = _userResolverService.GetUserRoles().Result;
            var response = await _mediator.Send(new AddOfficeCommand { DTO = dto, Roles = roles , UserId  = userId });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
