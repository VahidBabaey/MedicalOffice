using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.MenuDTO;
using MedicalOffice.Application.Features;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Features.MenuFeature.Requests.Queries;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MenuController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Menu>>> GetAllMenuItems([FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetAllMenuItemsQuery() { OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
