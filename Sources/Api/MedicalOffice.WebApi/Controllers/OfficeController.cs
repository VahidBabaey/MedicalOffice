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
    public class OfficeController : ControllerBase
    {
        private readonly IUserResolverService _userResolverService;
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator, IUserResolverService userResolverService)
        {
            _userResolverService = userResolverService;
            _mediator = mediator;
        }

        private new ObjectResult Response(BaseResponse response)
        {
            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<OfficeListDTO>>> GetByUserId()
        {
            var userId = _userResolverService.GetUserId().Result;

            var response = await _mediator.Send(new GetByUserIdQuery { UserId = Guid.Parse(userId) });

            return Response(response);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> AddOffice([FromBody] OfficeDTO dto)
        {
            var response = await _mediator.Send(new AddOfficeCommand { Dto = dto});

            return Response(response);
        }
    }
}
