using MediatR;
using MedicalOffice.Application.Dtos.MenuDTO;
using MedicalOffice.Application.Features.MenuFeature.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IMediator _mediator;
        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<List<MenuDto>>> GetMenuByUser([FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetMenuByUserQuery() { OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetAllMenu([FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetMenuQuery() { OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
