using MediatR;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Queries;
using MedicalOffice.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private new ObjectResult Response(BaseResponse response)
        {
            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet]
        public async Task<ActionResult<List<OfficeDTO>>> GetByUserId()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _mediator.Send(new GetByUserIdQuery { UserId = Guid.Parse(userId)});

            return Response(response);
        }
    }
}
