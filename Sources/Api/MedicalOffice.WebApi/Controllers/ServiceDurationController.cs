using MediatR;
using MedicalOffice.Application;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceDurationFeature.Requests.Commands;
using MedicalOffice.Application.Features.ServiceDurationScheduling.Requests.Commands;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDurationController : Controller
    {
        private readonly IMediator _mediator;

        public ServiceDurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(ServiceDurationDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new AddServiceDurationCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPatch]
        public async Task<ActionResult<Guid>> Update(EditServiceDurationDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new EditServiceDurationCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceDurationDetailsDTO>>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetAllServiceDurationQuery() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpDelete("list")]
        public async Task<IActionResult> RemoveList([FromBody] ServiceDurationIdsDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new DeleteServiceDurationListCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
