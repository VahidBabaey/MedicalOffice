using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Section;
using MedicalOffice.Application.Dtos.Service;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServiceController : Controller
{
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<Guid>> Create([FromBody] ServiceDTO dto)
    {
        var response = await _mediator.Send(new AddServiceCommand() { Dto = dto });

        return Ok(response);
    }

    [HttpDelete("DeleteService")]
    public async Task<IActionResult> RemoveAsync(Guid id)
    {
        var response = await _mediator.Send(new DeleteServiceCommand() { ServiceId = id });

        return Ok(response);
    }
    [HttpPost("UpdateService")]
    public async Task<ActionResult<Guid>> Update([FromBody] ServiceDTO dto)
    {
        var response = await _mediator.Send(new EditServiceCommand() { DTO = dto });

        return Ok(response);
    }

    [HttpGet("GetServices")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAll([FromQuery] ListDto dto, Guid sectionId)
    {
        var response = await _mediator.Send(new GetAllServicesBySectionIDQuery() { DTO = dto, SectionId = sectionId });

        return Ok(response);
    }
}