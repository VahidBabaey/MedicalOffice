using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Dtos.ServiceRoomDTO;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceDurationScheduling.Requests.Commands;
using MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Command;
using MedicalOffice.Application.Features.ServiceRoomFeature.Requests.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] AddServiceRoomDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new AddServiceRoomCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<ServiceRoomListDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officerId)
        {
            var response = await _mediator.Send(new GetAllServiceRoomsQuery() { DTO = dto, OfficeId = Guid.Parse(officerId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveList([FromBody] ServiceRoomIdsDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new DeleteServiceRoomCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateServiceRoomDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditServiceRoomCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
