using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Features.ServiceDurationScheduling.Requests.Commands;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDurationScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceDurationScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(ServiceDurationDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new AddServiceDurationCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceDurationListDTO>> GetAll([FromQuery] ListDto dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new GetAllServiceDurationQuery() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
