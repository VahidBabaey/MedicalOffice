using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Commands;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ServiceController : Controller
{
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] ServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new AddServiceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);

    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new DeleteServiceCommand() { OfficeId = Guid.Parse(officeId), ServiceId = id });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpPatch]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateServiceDTO dto, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new EditServiceCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    [HttpGet("names")]
    public async Task<ActionResult<List<ServiceIdNameDTO>>> GetAllServiceNames([FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllServiceNamesQuery() { OfficeId = Guid.Parse(officeId) });

        return StatusCode(Convert.ToInt32(response.StatusCode), response);
    }

    //TODO: return true status code not only OK!
    [HttpGet("section-services")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetAllBySectionId(Guid sectionId, [FromQuery] string officeId)
    {
        var response = await _mediator.Send(new GetAllServicesBySectionIDQuery() { SectionId = sectionId, OfficeId = Guid.Parse(officeId) });

        return Ok(response);
    }

    //TODO: return true status code not only OK!
    [HttpGet("Search")]
    public async Task<ActionResult<List<ServiceListDTO>>> GetSectionBySearch([FromQuery] string name, [FromQuery] string officeId, [FromQuery] string sectionId)
    {
        var response = await _mediator.Send(new GetServiceBySearchQuery() { Name = name, OfficeId = Guid.Parse(officeId), SectionId = Guid.Parse(sectionId) });

        return Ok(response);
    }
}